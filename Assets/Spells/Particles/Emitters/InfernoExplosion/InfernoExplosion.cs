using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoExplosion : MonoBehaviour
{
	AudioSource audioSource;
	[SerializeField] AudioClip explodeSFX;
	Collider2D col;
    IEnumerator LifeTime()
	{
		yield return new WaitForSeconds(0.2f);
		col.enabled = false;
		yield return new WaitForSeconds(2f);
		Destroy(this.gameObject);
	}
    // Start is called before the first frame update
    void Start()
    {
		col = this.GetComponent<Collider2D>();
		StartCoroutine(LifeTime());
		audioSource = this.GetComponent<AudioSource>();
		audioSource.PlayOneShot(explodeSFX);
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "RangedEnemy" || collision.gameObject.tag == "StrongEnemy")
		{
			collision.gameObject.GetComponent<HealthScript>().TakeDamage(5, collision.gameObject);
		}
		if (collision.gameObject.tag == "Boss")
		{
			collision.gameObject.GetComponent<Boss>().TakeDamage(5);
		}
	}
}
