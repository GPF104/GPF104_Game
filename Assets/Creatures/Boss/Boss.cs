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
    IEnumerator Tracker()
	{
        yield return new WaitForSeconds(0.25f);
        if (blip != null)
        {
            GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.UpdateBlipPosition(blip, this.transform.position);
        }
        StartCoroutine(Tracker());
    }
    private IEnumerator MoveToMiddle()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            if (!isAttacking)
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
        Player = GameObject.FindWithTag("Player");

        if (GameObject.FindObjectOfType<Boss_HealthUI>())
		{
            healthUI = GameObject.FindObjectOfType<Boss_HealthUI>().GetComponent<Boss_HealthUI>();
		}

        if (blip == null)
        {
            blip = GameObject.FindObjectOfType<GameManager>().uiHandler.uiMap.AddMapElement(BlipType.boss);
        }
        healthUI.SetBoss(this.gameObject);
        StartCoroutine(Tracker());
        targetPosition = GameObject.Find("Tower").transform.position;

        // Start the movement coroutine
        StartCoroutine(MoveToMiddle());
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
	}
}
