using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Timer : MonoBehaviour
{

	#region ExternalLinks

	UIHandler uiHandler;
	#endregion

	#region Attributes


	private TMP_Text TimerText;

	public void SetText(string input)
	{
		TimerText.text = input;
	}
	#endregion

	#region Unity

	// Start is called before the first frame update
	void Start()
    {
		uiHandler = GameObject.Find("UIHandler").GetComponent<UIHandler>();
        TimerText = this.gameObject.GetComponentInChildren<TMP_Text>();	
    }
	#endregion
}
