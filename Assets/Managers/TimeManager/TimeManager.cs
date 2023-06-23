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
	private bool active = true;
	private IEnumerator timer;
	private float secondCount, minuteCount;
	//	Methods
	IEnumerator Timer(float interval)
	{
		while (active)
		{
			yield return new WaitForSeconds(interval);
			secondCount += 1;
			minuteCount = (int)Mathf.Floor(secondCount / 60);

			gameManager.uiHandler.timer.SetText(GetTimeString(minuteCount, secondCount));
			yield return Timer(interval);
			Timer(interval);
		}
	}

	public void StartTimer(float interval)
	{
		secondCount = 0;
		minuteCount = 0;
		timer = Timer(interval);
		StartCoroutine(timer);
	}
	public void StopTimer()
	{
		active = false;
		StopCoroutine(timer);
	}

	public string GetTimeString(float minutes, float seconds)
	{
		//Return time string
		return minutes.ToString("0#") + ":" + (seconds % 60).ToString("0#");
	}

	public float GetTime()
	{
		return (minuteCount*60) + secondCount;
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
