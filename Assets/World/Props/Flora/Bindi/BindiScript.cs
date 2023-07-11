using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindiScript : MonoBehaviour
{
    HealthScript healthscript;
    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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