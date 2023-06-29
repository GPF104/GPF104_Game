using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region ExternalLinks
	public Player player;
	public UIHandler uiHandler;
	public TimeManager timeManager;

	public GameObject bubble;
	public GameObject spawner;

	public LevelGenerator levelGenerator;
	#endregion

	#region Attributes

	public bool GameFinished = false;
	public void GameOver()
	{
		GameFinished = true;
		Time.timeScale = 0;
	}

	#endregion

	#region Unity

	IEnumerator InitialLoad()
	{

		levelGenerator.GenerateLevel();
		yield return new WaitForSeconds(0.5f);
		timeManager.StartTimer(1);
	}

	// Start is called before the first frame update
	void Start()
    {
		uiHandler = this.uiHandler.GetComponent<UIHandler>();
		timeManager = this.timeManager.GetComponent<TimeManager>();
		levelGenerator = GameObject.FindObjectOfType<LevelGenerator>().GetComponent<LevelGenerator>();
		StartCoroutine(InitialLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion
}
