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

	public GameObject camera;

	public LevelGenerator levelGenerator;
	#endregion

	#region Attributes

	public int score = 0;
	public bool GameFinished = false;
	public bool GamePaused = false;

	public void AddScore(int input)
	{
		score += input;
		uiHandler.uiScore.SetText(score.ToString());
	}
	public void GameOver()
	{
		GameFinished = true;
		uiHandler.Display(uiHandler.gameOverObject, true);
		uiHandler.Display(uiHandler.uiHealth.gameObject, false);
		uiHandler.Display(uiHandler.uiMap.gameObject, false);
		//uiHandler.Display(uiHandler.uiTimer.gameObject, false);
		Time.timeScale = 0;
	}

	public void Pause()
	{
		Debug.Log("Paused");
		GamePaused = true;
		Time.timeScale = 0;
		uiHandler.Display(uiHandler.uiMenu, true);
	}
	public void Unpause()
	{
		uiHandler.Display(uiHandler.uiMenu, false);
		Debug.Log("Unpaused");
		GamePaused = false;
		Time.timeScale = 1;
	}

	public void MoveCamera(Vector2 position)
	{
		camera.transform.position = position;
	}
	#endregion

	#region Unity

	IEnumerator InitialLoad()
	{
		GameFinished = false;
		GamePaused = false;
		Time.timeScale = 1;
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
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		StartCoroutine(InitialLoad());
    }

	#endregion
}
