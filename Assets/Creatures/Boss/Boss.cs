using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Boss_HealthUI healthUI;
    GameManager gameManager;
    HealthScript health;


    GameObject blip; //Minimap blip
    [SerializeField] public int MAX_HEALTH = 250;
    [SerializeField] public int Health = 250;
    [SerializeField] int Damage = 15;

    GameObject Player;
    [SerializeField] float AttackThreshold = 10f;

    public float moveSpeed = 2.0f;
    public Vector3 targetPosition;

    bool isAttacking = false;
    [SerializeField] GameObject AttackObject;

    //Audio
    AudioSource audioSource;
    [SerializeField] AudioClip bossEntry;

    public float checkInterval = 3f; // Time interval to check for getting stuck
    public float unstuckDistance = 1f; // Minimum distance to move to consider the enemy unstuck
    public float maxUnstuckAttempts = 5; // Maximum number of attempts to get unstuck

    private Vector3 initialPosition;
    private float timeSinceLastMove;

    bool isStuck = false;

    private IEnumerator CheckForStuck()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            // Calculate the distance moved since the last check
            float distanceMoved = Vector3.Distance(transform.position, initialPosition);

            // If the distance moved is less than the unstuck distance
            if (distanceMoved < unstuckDistance)
            {
                isStuck = true;
                // Try to find a new position to get unstuck
                for (int i = 0; i < maxUnstuckAttempts; i++)
                {
                    Vector2 randomDirection = Random.insideUnitCircle.normalized * unstuckDistance;
                    Vector3 newTarget = transform.position + (Vector3)randomDirection;

                    if (CanMoveToPosition(newTarget))
                    {
                        float step = moveSpeed * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, newTarget, step);
                        break;
                    }
                }
            }
            else
            {
                // Reset the initial position and update the time since last move
                initialPosition = transform.position;
                timeSinceLastMove = 0f;
                isStuck = false;
            }
            isStuck = false;
        }
    }

    private bool CanMoveToPosition(Vector3 targetPosition)
    {
        // Cast a ray from the enemy's current position to the target position to check for obstacles
        Vector3 direction = targetPosition - transform.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(transform.position, direction, distance, LayerMask.GetMask("World")))
        {
            return false; // Obstacle detected, cannot move
        }

        return true;
    }
    IEnumerator Tracker()
	{
        
        yield return new WaitForSeconds(0.25f);
        if (blip != null)
        {
            GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.UpdateBlipPosition(blip, this.transform.position);
        }
        StartCoroutine(Tracker());
    }
    private IEnumerator MoveToPlayer()
    {
        
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            if (!isAttacking && !isStuck)
			{
                // Calculate the step to move towards the target position
                float step = moveSpeed * Time.deltaTime;

                // Move the boss towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                // Yield until the next frame
                yield return null;
            }
            else
			{
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    IEnumerator Die()
	{
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }

    public void Defeat()
	{
        StartCoroutine(Die());
	}

    public void TakeDamage(int damage)
    {
        Health += -damage;
        healthUI.SetHealth(Health);
        if (Health <= 0)
		{
            Defeat();
		}
    }

    float distance = 10;
    float secondCount = 0f;
    IEnumerator TrackPlayer(GameObject gobject)
	{
        yield return new WaitForSeconds(0.25f);
        targetPosition = gobject.transform.position;
        distance = Vector2.Distance(this.transform.position, gobject.transform.position);
        if (distance < 15)
		{
            secondCount += 0.25f;
            if (secondCount > 1.5)
			{
                Debug.Log("BOSS ATTACK");
                isAttacking = true;
                yield return new WaitForSeconds(0.5f);
                GameObject go = Instantiate(AttackObject);
                go.transform.position = gobject.transform.position;
            }
		}
		else
		{
            secondCount = 0f;
            isAttacking = false;
		}
        StartCoroutine(TrackPlayer(gobject));

    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        health = this.gameObject.GetComponent<HealthScript>();
        audioSource = this.GetComponent<AudioSource>();
        Player = GameObject.FindWithTag("Player");

        if (GameObject.FindObjectOfType<Boss_HealthUI>())
		{
            healthUI = GameObject.FindObjectOfType<Boss_HealthUI>().GetComponent<Boss_HealthUI>();
		}

        if (blip == null)
        {
            blip = GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.AddMapElement(BlipType.boss);
        }
        if (bossEntry != null)
		{
            audioSource.PlayOneShot(bossEntry);
		}
        healthUI.SetBoss(this.gameObject);
        StartCoroutine(Tracker());
        

        // Start the movement coroutine
        StartCoroutine(MoveToPlayer());
        StartCoroutine(CheckForStuck());
        StartCoroutine(TrackPlayer(Player));
    }

    void OnDestroy()
    {
        Destroy(blip);
        healthUI.SetHealth(MAX_HEALTH);
        gameManager.bossManager.Defeated();
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
            Debug.Log("Boss Hit");
            TakeDamage(collision.gameObject.GetComponent<ProjectileScript>().damage);
		}
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(15);
        }
    }
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "World" && isStuck == true)
		{
            Debug.LogWarning("Boss destroyed a tree.");
            Vector2 playerDirection = (targetPosition - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, Mathf.Infinity, LayerMask.GetMask("TreeLayer"));

            if (hit.collider != null && hit.collider.CompareTag("World"))
            {
                GameObject go = Instantiate(AttackObject);
                go.transform.position = hit.collider.gameObject.transform.position;
                Debug.LogWarning("Boss destroyed a tree.");
            }
        }
	}
}
