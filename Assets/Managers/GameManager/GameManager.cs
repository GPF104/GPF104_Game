using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region ExternalLinks
	public Player player;
	public UIHandler uiHandler;
	public TimeManager timeManager;
	public MainCamera mainCamera;
	public AudioManager audioManager;
	public GameObject bubble;
	public GameObject spawner;


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
		mainCamera.StopShake();
		uiHandler.Display(uiHandler.gameOverObject, true);
		uiHandler.Display(uiHandler.uiHealth.gameObject, false);
		uiHandler.Display(uiHandler.uiMap.gameObject, false);
		uiHandler.uiGameOver.GetComponent<UI_GameOver>().SetText("Time: " + timeManager.CurrentTime() + " \n Score: " + score);
		//uiHandler.Display(uiHandler.uiTimer.gameObject, false);
		Time.timeScale = 0;
	}
	public void PrepareGame()
	{
		//Time.timeScale = 0;
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

	IEnumerator PlayGame()
	{
		yield return new WaitForSeconds(0.5f);
		audioManager.Initialize();
		timeManager.StartTimer(1);
		GamePaused = false;
		GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeOut();
	}
	[ContextMenu("StartGame")]
	public void StartGame()
	{
		StartCoroutine(PlayGame());
	}
	#endregion

	#region Unity

	IEnumerator InitialLoad()
	{
		PrepareGame();
		GameFinished = false;
		GamePaused = true;
		levelGenerator.GenerateLevel(); // Generates the level
		yield return new WaitForSeconds(0.5f);
		
		//timeManager.StartTimer(1);
	}

	// Start is called before the first frame update
	void Start()
    {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
		uiHandler = this.uiHandler.GetComponent<UIHandler>();
		timeManager = this.timeManager.GetComponent<TimeManager>();
		audioManager = this.audioManager.GetComponent<AudioManager>();
		levelGenerator = GameObject.FindObjectOfType<LevelGenerator>().GetComponent<LevelGenerator>();
		StartCoroutine(InitialLoad());
    }

	#endregion
}
