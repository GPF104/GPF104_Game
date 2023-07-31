using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour
{



	#region ExternalLinks

	GameObject[] UI;

	public GameManager gameManager;
	public UI_Timer uiTimer;
	public UI_Score uiScore;
	public Overlay overlay;
	public FrameControls frameControls;
	public UI_Map uiMap;
	public UI_Health uiHealth;
	public UI_GameOver uiGameOver;
	public GameObject gameOverObject;
	public GameObject uiMenu;
	public ScrollCounter scrollCounter;

	[SerializeField] GameObject eventSystemObject;

	private EventSystem eventSystem;

	#endregion



	#region Attributes
	public enum Fade
	{
		fadeout = 0,
		fadein = 1
	}
	public void EnableEventController(bool enabled)
	{
		if (eventSystem != null)
		{
			eventSystem.enabled = enabled;
		}
		else
		{
			Debug.LogWarning("eventSystemController is null. Make sure eventSystemObject is assigned correctly.");
		}
	}
	public void Display(GameObject frame, bool active)
	{
		frame.SetActive(active);
	}
	IEnumerator FadeOut(float speed)
	{
		yield return new WaitForSeconds(speed);
	}
	IEnumerator FadeIn(float speed)
	{
		yield return new WaitForSeconds(speed);
	}

	public void FrameFade(GameObject gobject, Fade fade, float speed)
	{

	}

	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		if (eventSystemObject != null)
		{
			eventSystem = eventSystemObject.GetComponent<EventSystem>();
		}
		else
		{
			Debug.LogWarning("eventSystemObject is not assigned in the Inspector.");
		}

		gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
		uiTimer = GameObject.FindObjectOfType<UI_Timer>().GetComponent<UI_Timer>();
		uiScore = GameObject.FindObjectOfType<UI_Score>().GetComponent<UI_Score>();
		overlay = GameObject.FindObjectOfType<Overlay>().GetComponent<Overlay>();
		frameControls = this.GetComponent<FrameControls>();
		uiMap = GameObject.FindObjectOfType<UI_Map>().GetComponent<UI_Map>();
		uiHealth = GameObject.FindObjectOfType<UI_Health>().GetComponent<UI_Health>();
		scrollCounter = GameObject.FindObjectOfType<ScrollCounter>().GetComponent<ScrollCounter>();

		gameOverObject.SetActive(false);
		uiMenu.SetActive(false);
		
    }

	#endregion

}

