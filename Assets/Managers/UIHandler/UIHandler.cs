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
	public UI_Timer timer;
	public Overlay overlay;
	public FrameControls frameControls;
	#endregion

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
		timer = GameObject.FindObjectOfType<UI_Timer>().GetComponent<UI_Timer>();
		overlay = GameObject.FindObjectOfType<Overlay>().GetComponent<Overlay>();
		frameControls = this.GetComponent<FrameControls>();
    }

	#endregion
}
