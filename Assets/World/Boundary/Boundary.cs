using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] float radius = 5;

    CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = this.GetComponent<CircleCollider2D>();
        
        circleCollider.radius = radius;
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
            Debug.Log("Player leaving level bounds");
		}
	}

}
