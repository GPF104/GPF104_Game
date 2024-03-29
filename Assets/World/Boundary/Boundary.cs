using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
	#region ExternalLinks

	GameManager gameManager;
	[SerializeField] GameObject target;
	[SerializeField] GameObject overlay;
	#endregion

	#region Attributes

	float distance = 0;
	float distanceFade = 0;
	bool inarena = true;
	[SerializeField] public int MAX_DISTANCE = 80;
	[SerializeField] public int MIN_DISTANCE = 50;
	private bool canDamage = false;
	private int dmgCounter = 0;
	IEnumerator BoundDamage()
	{
		dmgCounter++;
		yield return new WaitUntil(() => canDamage == true);
		yield return new WaitForSeconds(1f);

		Debug.Log("DAMAGE OUT OF BOUNDS");
		GameObject.FindObjectOfType<Player>().TakeDamage(5);

		//Wait another half second before allowing the player to be damaged again.
		yield return new WaitForSeconds(0.5f);
		dmgCounter = 0;


	}
	IEnumerator OutofBounds()
	{
		yield return new WaitUntil(() => distance > MAX_DISTANCE);
		Debug.Log(distance + " in arena? " + inarena + " distance opacity " + distanceFade);
		inarena = false;
		canDamage = true;
		StartCoroutine(InBounds());
		yield return new WaitForSeconds(1.5f);
	}

	IEnumerator InBounds()
	{
		yield return new WaitUntil(() => distance < MAX_DISTANCE);
		inarena = true;
		canDamage = false;
		yield return new WaitForSeconds(1.5f);
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
		if (dmgCounter == 0)
		{
			StartCoroutine(BoundDamage());
		}
	}
	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
		if (target != null)
		{
			StartCoroutine(GetDistance());
		}
    }
	#endregion
}
