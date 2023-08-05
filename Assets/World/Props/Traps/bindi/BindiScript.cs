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

    //For sound
    [SerializeField] AudioClip SFX;
    AudioSource audioSource;

    private void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        if (gameObject.GetComponent<Animator>() != null)
        {
            animator= gameObject.GetComponent<Animator>();
            bc2d = gameObject.GetComponent<BoxCollider2D>();
            StartCoroutine(StayOpen());
        }        
    }


    float distance;
    bool isOpen = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (animator != null)
		{
            if (collision.gameObject.tag == "Player")
            {
                distance = Vector2.Distance(this.transform.position, collision.gameObject.transform.position);
                animator.SetFloat("distance", distance);
                if (distance < animator.GetFloat("minDist"))
                {
                    isOpen = true;
                    if (audioSource != null && SFX != null)
					{
                        StartCoroutine(PlaySound());
                    }
                }
                else
                {
                    isOpen = false;
                }
            }
        }
    }    
    IEnumerator PlaySound()
	{
        if (isOpen)
		{
            audioSource.PlayOneShot(SFX);
            yield return new WaitForSeconds(SFX.length + 0.15f);
            StartCoroutine(PlaySound());
        }
    }
    IEnumerator StayOpen()
    {
        yield return new WaitForSeconds(0.15f);
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