using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region ExternalLinks
	public Player player;
	public UIHandler uiHandler;
	public TimeManager timeManager;
	#endregion

	#region Attributes

	#endregion

	#region Unity

	IEnumerator InitialLoad()
	{
		yield return new WaitForSeconds(0.5f);


		timeManager.StartTimer(1);
	}

	// Start is called before the first frame update
	void Start()
    {
		uiHandler = this.uiHandler.GetComponent<UIHandler>();
		timeManager = this.timeManager.GetComponent<TimeManager>();
		StartCoroutine(InitialLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion
}
