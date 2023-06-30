using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
	Rigidbody2D rb2d;
	bool invincible = true;
	public int value = 50;
	public void SetTarget(Vector2 position, float time)
	{
		invincible = false;
		rb2d.velocity = (position - (Vector2)this.transform.position) * 0.1f;
	}

    // Start is called before the first frame update
    void Start()
    {
		rb2d = this.GetComponent<Rigidbody2D>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Spawner")
		{
            Debug.Log("Spawner hit by bubble");
			Destroy(this.gameObject);
		}
		if (collision.tag == "Bullet" && !invincible)
		{
			GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>().AddScore(value);
			Destroy(collision.gameObject);
			Destroy(this.gameObject);
		}
	}
}
