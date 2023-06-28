using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject enemyProjectile;

    public float shootingDistance = 5f;
    public float stoppingDistance = 3f;

    public float fireRate;
    private float timeToFire;
    public Transform firingPoint;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        timeToFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        if (Vector2.Distance(player.position, transform.position) <= shootingDistance)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (timeToFire <= 0f)
        {
            Instantiate(enemyProjectile, firingPoint.position, firingPoint.rotation);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
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

    void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
