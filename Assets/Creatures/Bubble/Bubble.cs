using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
	#region ExternalLinks

	public GameObject particlePrefab;
	private GameObject particleObject;
	[SerializeField] GameObject BurstFX;
	#endregion

	#region Attributes
	Rigidbody2D rb2d;
	bool invincible = true;
	public int value = 50;
	public void SetTarget(Vector2 position, float time)
	{
		invincible = false;
		rb2d.velocity = (position - (Vector2)this.transform.position) * 0.1f;
	}

	void Kill()
	{
		particleObject.GetComponent<ParticleEmitter>().Remove();
		Debug.Log("Bubble burst");
		GameObject go = Instantiate(BurstFX);
		go.transform.position = this.transform.position;
		Destroy(gameObject); // Destroy the bullet
	}

	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		rb2d = this.GetComponent<Rigidbody2D>();
		particleObject = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
    }

	void Update()
	{
		particleObject.transform.position = this.transform.position;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Spawner")
		{
            Debug.Log("Spawner hit by bubble");
			Kill();
		}
		if (collision.tag == "Bullet" && !invincible)
		{
			GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>().AddScore(value);
			Kill();
		}
	}
	#endregion
}
