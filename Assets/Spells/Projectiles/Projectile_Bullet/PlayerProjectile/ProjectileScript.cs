using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    #region ExternalLinks

    HealthScript healthScript;

	#endregion

	#region Attributes

	IEnumerator LifeSpan(float interval)
	{
        yield return new WaitForSeconds(interval);
        Destroy(this.gameObject);
    }
	#endregion

	#region Unity

	void Start()
    {
        StartCoroutine(LifeSpan(3));
    }

    private bool ishit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Enemy" || collision.collider.tag == "RangedEnemy" || collision.collider.tag == "StrongEnemy" && ishit == false)
		{
            ishit = true;            
            healthScript = collision.gameObject.GetComponent<HealthScript>();
            healthScript.TakeDamage(1, collision.gameObject);
            Destroy(this.gameObject);
        }
        else
		{
            Debug.Log("Hit world");
            Destroy(this.gameObject);
        }

        //check here to see if hitting enemy
    }
	#endregion
}
