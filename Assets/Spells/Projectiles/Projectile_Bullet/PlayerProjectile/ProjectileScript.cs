using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    #region ExternalLinks

    HealthScript healthScript;

    public GameObject particlesPrefab;
    private GameObject particleObject;


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
        particleObject.GetComponent<ParticleEmitter>().Remove();
        Destroy(gameObject); // Destroy the bullet
    }

    void Start()
    {
        StartCoroutine(LifeSpan(3));
        particleObject = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
    }
	void Update()
	{
		particleObject.transform.position = this.transform.position;
	}


	private bool ishit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Enemy" || collision.collider.tag == "RangedEnemy" || collision.collider.tag == "StrongEnemy" && ishit == false)
		{
            ishit = true;            
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1, collision.gameObject);
            Kill();
        }
<<<<<<< HEAD
        else
		{
            Debug.Log("Hit world");
            Kill();
        }

=======
        else if (collision.collider.tag == "RangedEnemy" && ishit == false)
        {
            ishit = true;
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1, collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.collider.tag == "RangedEnemy" && ishit == false)
        {
            ishit = true;
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1, collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.collider.tag == "Environment" && ishit == false)
        {
            ishit = true;
            Destroy(this.gameObject);
        }
>>>>>>> Sean
        //check here to see if hitting enemy
    }
    #endregion
}
