using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

	public enum Fade
	{
		fadeout = 0,
		fadein = 1
	}

	#region ExternalLinks

	GameObject[] UI;

	public GameManager gameManager;
	public UI_Timer uiTimer;
	public Overlay overlay;
	public FrameControls frameControls;
	public UI_Map uiMap;
	public UI_Health uiHealth;
	public UI_GameOver uiGameOver;
	public GameObject gameOverObject;
	public GameObject uiMenu;

	#endregion

	public void Display(GameObject frame, bool active)
	{
		frame.SetActive(active);
	}

	#region Attributes
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
		gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
		uiTimer = GameObject.FindObjectOfType<UI_Timer>().GetComponent<UI_Timer>();
		overlay = GameObject.FindObjectOfType<Overlay>().GetComponent<Overlay>();
		frameControls = this.GetComponent<FrameControls>();
		uiMap = GameObject.FindObjectOfType<UI_Map>().GetComponent<UI_Map>();
		uiHealth = GameObject.FindObjectOfType<UI_Health>().GetComponent<UI_Health>();

		gameOverObject.SetActive(false);
		uiMenu.SetActive(false);
		
    }

	#endregion

}

