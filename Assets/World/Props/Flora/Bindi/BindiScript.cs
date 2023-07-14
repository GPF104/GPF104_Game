using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindiScript : MonoBehaviour
{
    HealthScript healthscript;
    public int damage = 2;


    private bool ishit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && ishit == false)
        {
            ishit = true;
            Destroy(this.gameObject);
        }
    }
}