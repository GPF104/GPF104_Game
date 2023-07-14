using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Inferno : MonoBehaviour
{
    Vector2 destination = new Vector2(0, 0);

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] GameObject explosion;
    Rigidbody2D rb2d;
    float lerpT = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = ((Vector3)destination - transform.position).normalized;
        lerpT += moveSpeed * Time.fixedDeltaTime;

        Vector3 currentPosition = transform.position;
        Vector3 lerpedPosition = Vector3.Lerp(currentPosition, destination, lerpT);

        rb2d.MovePosition(lerpedPosition);

        if (Vector3.Distance(transform.position, destination) < 0.15f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
