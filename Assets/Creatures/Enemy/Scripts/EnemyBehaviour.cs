using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	#region ExternalLinks

	public Transform player;

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

    void moveEnemy(Vector2 direction)
    {
        if (!isStuck && !canAttack)
		{
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

    IEnumerator CoolDown(float cooldown)
	{
        yield return new WaitForSeconds(cooldown);
        hitPlayer = false;
    }
    IEnumerator Attack()
    {
        if (canAttack)
        {
            yield return new WaitForSeconds(0.05f);
            Debug.Log("ATTACK");

            // Calculate the direction from the enemy to the player
            Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

            // Set the enemy's velocity to lunge towards the player
            rb.velocity = direction * 10;
            Debug.Log("Lunge");
            // Wait for a short duration before resetting the velocity
            yield return new WaitForSeconds(0.5f);

            // Reset the enemy's velocity
            canAttack = false;
            rb.velocity = Vector2.zero;

        }
        else
		{
            canAttack = false;
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

    IEnumerator IsStuck()
	{
        oldPosition = transform.position;
        yield return new WaitForSeconds(0.25f);
        float distance = Vector2.Distance(oldPosition, transform.position);
        if (distance < 0.5f && !canAttack)
		{
            isStuck = true;
		}
		else
		{
            isStuck = false;
		}

        StartCoroutine(IsStuck());
    }
    #endregion

    #region Unity

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(IsStuck());
        StartCoroutine(CanAttack());
    }

    // Update is called once per frame
    void Update()
    {
        movement = player.position - transform.position;
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        movement.Normalize();
    }

    private void FixedUpdate()
    {
        if (!canAttack && !isStuck)
		{
            if (Vector2.Distance(player.position, transform.position) >= 2)
			{
                moveEnemy(movement);
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
	#endregion
}
