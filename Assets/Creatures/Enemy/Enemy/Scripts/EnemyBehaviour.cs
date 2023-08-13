using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	#region ExternalLinks

	public Transform player;
    GameManager gameManager;

	[SerializeField] List<GameObject> Drops = new List<GameObject>();
    [SerializeField] int spawnChance = 20;
    #endregion

    #region Attributes

    public int scoreValue = 15;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 oldPosition;
    private bool isStuck = false;
    private bool canAttack = false;
    public int damage = 5;
    private bool hitPlayer = false;
    [SerializeField] float cooldown = 1.0f;

    AudioSource audioSource;
    [SerializeField] List<AudioClip> attackSounds = new List<AudioClip>();

    //Minimap blip

    GameObject blip;
    void moveEnemy(Vector2 direction)
    {
        if (!isStuck && !canAttack)
		{
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
            rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            
        }
    }

    IEnumerator CoolDown(float cooldown)
	{
        yield return new WaitForSeconds(cooldown);
        hitPlayer = false;
    }
    bool isAttacking = false;
    IEnumerator Attack()
    {
        if (canAttack)
        {
            if (!isAttacking)
			{
                if (audioSource != null)
				{
					audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)]);
                    isAttacking = true;
                }
			}
            yield return new WaitForSeconds(0.05f);

            // Calculate the direction from the enemy to the player
            Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

            // Set the enemy's velocity to lunge towards the player
            rb.velocity = direction * 10;
            rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            // Wait for a short duration before resetting the velocity
            yield return new WaitForSeconds(0.5f);

            // Reset the enemy's velocity
            canAttack = false;
            isAttacking = false;
            rb.velocity = Vector2.zero;

        }
        else
		{
            canAttack = false;
            isAttacking = false;
		}
    }

    IEnumerator CanAttack()
	{
        yield return new WaitForSeconds(0.25f);
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= 2.5f)
		{
            canAttack = true;
            StartCoroutine(Attack());
        }
        else
		{
            canAttack = false;
        }

        StartCoroutine(CanAttack());
    }
    float stuckTime = 0;
    IEnumerator IsStuck()
	{
        if (blip != null)
		{
            GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.UpdateBlipPosition(blip, this.transform.position);
        }
        if (stuckTime > 10)
		{
            Debug.LogWarning("Enemy stuck for longer than 10 seconds, removing");
            Destroy(this.gameObject);
		}
        oldPosition = transform.position;
        yield return new WaitForSeconds(0.25f);
        float distance = Vector2.Distance(oldPosition, transform.position);
        if (distance < 0.5f)
		{
            isStuck = true;
            stuckTime += 0.25f;
		}
		else
		{
            isStuck = false;
            stuckTime = 0;
		}

        StartCoroutine(IsStuck());
    }

    IEnumerator Spawn()
	{
        yield return new WaitForSeconds(0.25f);
        if (blip == null)
        {
            blip = GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.AddMapElement(BlipType.enemy);
        }
        StartCoroutine(IsStuck());
        StartCoroutine(CanAttack());
    }
	#endregion

	#region Unity

	void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        audioSource = this.GetComponent<AudioSource>();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        movement = player.position - transform.position;
        //float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        movement.Normalize();
    }

    private void FixedUpdate()
    {
        if (!canAttack && !isStuck && this.gameObject.GetComponent<HealthScript>().isAlive == true)
		{
            if (Vector2.Distance(player.position, transform.position) >= 2)
			{
                moveEnemy(movement);
                UpdateSortingOrder();
            }
        }
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && !hitPlayer)
		{
            Debug.Log("Hit Player");
            hitPlayer = true;
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            StartCoroutine(CoolDown(cooldown));
		}

	}
    private void UpdateSortingOrder()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the player's y-position and the tree object's y-position
        float enemyY = this.gameObject.transform.position.y;
        float treeY = transform.position.y;

        // Set the initial sorting order based on the y-position and enemy position relative to the tree object
        int sortingOrder = Mathf.RoundToInt(-treeY * 100f);
        if (enemyY > treeY)
        {
            sortingOrder -= 1;
        }
        spriteRenderer.sortingOrder = sortingOrder;
    }
    void OnDestroy()
    {
        Destroy(blip);
    }
    #endregion
}
