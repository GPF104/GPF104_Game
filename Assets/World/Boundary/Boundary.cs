using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] float radius = 5;

    CircleCollider2D circleCollider;
	private bool outofbounds = false;

	IEnumerator OutOfBounds()
	{
		yield return new WaitUntil(() => outofbounds == true);
		Debug.Log("Out of bounds");
		StartCoroutine(OutOfBounds());
	}


    // Start is called before the first frame update
    void Start()
    {
        circleCollider = this.GetComponent<CircleCollider2D>();
        circleCollider.radius = radius;

		StartCoroutine(OutOfBounds());
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			outofbounds = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player" && outofbounds == true)
		{
			outofbounds = false;
		}
	}
}
