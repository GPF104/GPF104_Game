using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
	GameManager gameManager;
	[SerializeField] GameObject target;
	[SerializeField] GameObject overlay;
	float distance = 0;
	float distanceFade = 0;
	bool inarena = true;
	[SerializeField] public int MAX_DISTANCE = 80;
	[SerializeField] public int MIN_DISTANCE = 50;
	IEnumerator OutofBounds()
	{
		yield return new WaitUntil(() => distance > MAX_DISTANCE);
		Debug.Log(distance + " in arena? " + inarena + " distance opacity " + distanceFade);
		inarena = false;
		StartCoroutine(InBounds());
		yield return new WaitForSeconds(1.5f);

		gameManager.uiHandler.frameControls.FrameFade(overlay, Fade.In, 1);
	}

	IEnumerator InBounds()
	{
		yield return new WaitUntil(() => distance < MAX_DISTANCE);
		inarena = true;
		yield return new WaitForSeconds(1.5f);
		gameManager.uiHandler.frameControls.FrameFade(overlay, Fade.Out, 1);
	}

	IEnumerator DistanceFade(float distance)
	{
		yield return new WaitForSeconds(0.5f);
		if (distance > MIN_DISTANCE)
		{
			distanceFade = (distance - MIN_DISTANCE) / (MAX_DISTANCE - MIN_DISTANCE);
			distanceFade = Mathf.Clamp01(distanceFade); // Ensure it stays between 0 and 1

			gameManager.uiHandler.frameControls.FrameOpacity(overlay, distanceFade);
		}
	}
	IEnumerator GetDistance()
	{
		yield return new WaitForSeconds(0.15f);
		distance = Vector2.Distance(this.transform.position, target.transform.position);
		//
		StartCoroutine(DistanceFade(distance));
		StartCoroutine(GetDistance());
		StartCoroutine(OutofBounds());
		

	}

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
		if (target != null)
		{
			StartCoroutine(GetDistance());
		}
		
    }
}
