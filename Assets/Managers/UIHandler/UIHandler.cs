using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
	#region ExternalLinks

	GameManager gameManager;
	public UI_Timer timer;

	#endregion

	#region Attributes

	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		timer = this.gameObject.GetComponentInChildren<UI_Timer>();
    }

	#endregion
}
