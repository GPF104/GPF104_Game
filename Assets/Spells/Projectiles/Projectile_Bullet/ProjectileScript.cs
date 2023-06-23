using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	#region ExternalLinks

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        //check here to see if hitting enemy
    }
	#endregion
}
