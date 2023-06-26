using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	#region ExternalLinks

	#endregion

	#region Attributes

	Rigidbody2D rb2d;
	Transform target;
	float moveSpeed = 2.5f;
	Vector2 newPosition = new Vector2(0, 0);

	Vector2 position(Transform target)
	{
		return (Vector2)target.localPosition;
	}

	bool MoveLogic()
	{
		int random = Random.Range(0, 100);

		if (random > 50)
			return true;
		return false;
	}

	#endregion

	#region Unity

	// Start is called before the first frame update
	void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		target = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		newPosition = position(target) - (Vector2)this.transform.position;
    }

	void FixedUpdate()
	{
		if (MoveLogic())
		{
			this.rb2d.velocity = newPosition * Time.deltaTime * moveSpeed;
			this.transform.up = position(target) - (Vector2)this.transform.position;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		
	}
	#endregion
}
