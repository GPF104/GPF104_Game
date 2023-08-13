using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    #region ExternalLinks

    public Transform player;
    public GameObject enemyProjectile;
    #endregion

    #region Attributes

    // Enemy Attributes
    public float moveSpeed = 5f;
    
    private Vector2 movement;


    public float shootingDistance = 5f;
    public float stoppingDistance = 3f;

    public float fireForce = 20f;
    public float fireRate;
    [SerializeField] float timeToFire = 0.2f;
    [SerializeField] float maxfireDelay = 1.2f;
    private Rigidbody2D rb;
    public Transform firePoint;

    private bool isStuck = false;
    private Vector2 oldPosition;

    GameObject blip;
    #endregion


    IEnumerator Fire()
	{
        yield return new WaitForSeconds(0.1f);
        GameObject bullet = Instantiate(enemyProjectile, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    IEnumerator ShootChecker()
	{
        yield return new WaitForSeconds(Random.Range(timeToFire, maxfireDelay));
        if (Vector2.Distance(player.position, transform.position) <= Random.Range(shootingDistance, 10) && !isStuck && this.GetComponent<HealthScript>().isAlive)
        {
            StartCoroutine(Fire());
        }
        StartCoroutine(ShootChecker());
    }

    void moveEnemy(Vector2 direction)
    {
        if (!isStuck && this.GetComponent<HealthScript>().isAlive == true)
		{
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
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
        StartCoroutine(ShootChecker());
        StartCoroutine(IsStuck());
    }

    #region Unity
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        timeToFire = 0f;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.position, transform.position) >= stoppingDistance)
                moveEnemy(movement);
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

	private void OnDestroy()
	{
        Destroy(blip);
	}
	#endregion
}
