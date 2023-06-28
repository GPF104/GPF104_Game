using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    HealthScript healthScript;

    IEnumerator LifeSpan(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(this.gameObject);
    }

    void Start()
    {
        StartCoroutine(LifeSpan(3));
    }

    private bool ishit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && ishit == false)
        {
            ishit = true;
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1, collision.gameObject);
            Destroy(this.gameObject);
        }
        //check here to see if hitting enemy
    }
}
