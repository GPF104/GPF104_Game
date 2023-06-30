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
        if (Vector2.Distance(player.position, transform.position) <= Random.Range(shootingDistance, 10) && !isStuck)
        {
            StartCoroutine(Fire());
        }
        StartCoroutine(ShootChecker());
    }

    void moveEnemy(Vector2 direction)
    {
        if (!isStuck)
		{
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

    IEnumerator IsStuck()
    {
        oldPosition = transform.position;
        yield return new WaitForSeconds(0.25f);
        float distance = Vector2.Distance(oldPosition, transform.position);

        if (distance < 0.5f)
        {
            isStuck = true;
        }
        else
        {
            isStuck = false;
        }
        StartCoroutine(IsStuck());
    }

    #region Unity
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        timeToFire = 0f;
        StartCoroutine(ShootChecker());
        StartCoroutine(IsStuck());
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
	#endregion


}
