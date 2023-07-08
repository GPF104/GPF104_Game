using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoScript : MonoBehaviour
{
    #region ExternalLinks

    HealthScript healthScript;

   // public GameObject particlesPrefab;
    //private GameObject particleObject;


    #endregion

    #region Attributes

    IEnumerator LifeSpan(float interval)
    {
        yield return new WaitForSeconds(interval);
        Kill();
    }
    #endregion

    #region Unity
    void Kill()
    {
       // particleObject.GetComponent<ParticleEmitter>().Remove();
        Destroy(gameObject); // Destroy the bullet
    }

    void Start()
    {
        StartCoroutine(LifeSpan(3));
        //particleObject = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
    }
    void Update()
    {
        //particleObject.transform.position = this.transform.position;
    }


    private bool ishit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy" || collision.tag == "RangedEnemy" || collision.tag == "StrongEnemy" && ishit == false)
        {
            ishit = true;
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1000, collision.gameObject);
            Kill();
        }
        else
        {
            Debug.Log("Hit world");
            Kill();
        }

        //check here to see if hitting enemy
    }
    #endregion
}
