using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	#region ExternalLinks

	GameManager gameManager;
	UIHandler uiHandler;

	#endregion

	#region Attributes

	private float time = 0;
	private bool active = false;
	IEnumerator Timer(float interval)
	{
		while (active)
		{
			yield return new WaitForSeconds(interval);
			time += 1;
		}
	}

	public void Start(float interval)
	{
		StartCoroutine(Timer(interval));
	}

	public float GetTime()
	{
		return time;
	}
	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		gameManager = this.GetComponentInParent<GameManager>();
		uiHandler = this.GetComponentInParent<UIHandler>();
    }

	#endregion
}
