using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindiDamage : MonoBehaviour
{
    // Start is called before the first frame update
    BindiScript Bindi;
    CircleCollider2D circleCollider;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        Bindi = this.GetComponentInParent<BindiScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other is CircleCollider2D)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().TakeDamage(Bindi.damage);
            }
        }
    }
}
