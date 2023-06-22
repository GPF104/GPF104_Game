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

	//	Variables
	private float time = 0;
	private bool active = true;
	private IEnumerator timer;
	//	Methods
	IEnumerator Timer(float interval)
	{
		Debug.Log("Timer Started");
		while (active)
		{
			yield return new WaitForSeconds(interval);
			time += 1;
			gameManager.uiHandler.timer.SetText(time.ToString());
			Timer(interval);
		}
	}

	public void StartTimer(float interval)
	{
		timer = Timer(interval);
		StartCoroutine(timer);
	}
	public void StopTimer()
	{
		StopCoroutine(timer);
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
