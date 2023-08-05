using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindiScript : MonoBehaviour
{
    //for player script to call for damage
    HealthScript healthscript;
    public int damage = 2;
    CircleCollider2D cc2d;

    //for animations
    Animator animator;
    BoxCollider2D bc2d;    

    private void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        if (gameObject.GetComponent<Animator>() != null)
        {
            animator= gameObject.GetComponent<Animator>();
            bc2d = gameObject.GetComponent<BoxCollider2D>();
            StartCoroutine(StayOpen());
        }        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {        
        if(other is CircleCollider2D)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }

    float distance;
    bool isOpen = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            if (collision.gameObject.tag == "Player" && collision == bc2d)
            {
                distance = Vector2.Distance(this.transform.position, collision.gameObject.transform.position);
                animator.SetFloat("distance", distance);
                Debug.Log("PlayerDetected" + distance);
                if (distance < 11)
                {
                    isOpen = true;
                }
                else
                {
                    isOpen = false;
                }
            }
        }        
    }    
    IEnumerator StayOpen()
    {
        yield return new WaitForSeconds(1);
        if (distance <= 5 && isOpen == true)
        {
            animator.SetInteger("openState", 1);
        }
        else
        {
            animator.SetInteger("openState", 2);
        }
        StartCoroutine(StayOpen());
    }
}