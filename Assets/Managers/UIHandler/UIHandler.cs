using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
	#region ExternalLinks

	GameObject[] UI;

	GameManager gameManager;
	public UI_Timer timer;
	public Overlay overlay;

	#endregion

	#region Attributes

	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		timer = GameObject.FindObjectOfType<UI_Timer>().GetComponent<UI_Timer>();
    }

	#endregion
}
