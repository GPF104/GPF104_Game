using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoExplosion : MonoBehaviour
{

    IEnumerator LifeTime()
	{
		yield return new WaitForSeconds(0.2f);
		Destroy(this.gameObject);
	}
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(LifeTime());
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangedEnemy" || collision.gameObject.tag == "StrongEnemy")
		{
			collision.gameObject.GetComponent<HealthScript>().TakeDamage(5, collision.gameObject);
		}
	}
}
