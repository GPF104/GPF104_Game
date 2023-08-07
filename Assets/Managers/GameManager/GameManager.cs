using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public BossManager bossManager;

	[SerializeField] MusicPlayer musicPlayer;

	[SerializeField] bool isDev = false;

	public LevelGenerator levelGenerator;
	#endregion

	#region Attributes

	EventSystemController eventSystemController;

	public int score = 0;
	public bool GameFinished = false;
	public bool GamePaused = false;
	private bool isInBackground = true;

	public void AddScore(int input)
	{
		score += input;
		uiHandler.uiScore.SetText(score.ToString());
	}

	[ContextMenu("GameOver")]
	public void GameOver()
	{
		GameFinished = true;
		GamePaused = true;
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
		if (!isInBackground)
		{
			Debug.Log("Paused");
			GamePaused = true;
			Time.timeScale = 0;
			uiHandler.Display(uiHandler.uiMenu, true);
		}
	}
	public void Unpause()
	{
		if (!isInBackground)
		{
			uiHandler.Display(uiHandler.uiMenu, false);
			Debug.Log("Unpaused");
			GamePaused = false;
			Time.timeScale = 1;
		}
	}

	IEnumerator PlayGame()
	{
		yield return new WaitForSeconds(0.5f);
		musicPlayer.SetEnabled(true);
		uiHandler.EnableEventController(true);
		isInBackground = false;
		audioManager.Initialize();
		timeManager.StartTimer(1);
		GamePaused = false;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
		GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeOut();

		yield return new WaitForSeconds(0.5f);
		musicPlayer.SetClip(audioManager.RandomMusic());
	}
	[ContextMenu("StartGame")]
	public void StartGame()
	{
		StartCoroutine(PlayGame());
	}

	[ContextMenu("SpawnBoss")]
	public void SpawnBoss()
	{
		bossManager.Initialize();
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
		if (isDev)
		{
			StartGame();
		}
		bool arena = false;
		bool dontdestroy = false;

		if (SceneManager.sceneCount == 1)
		{
			if (SceneManager.GetSceneAt(0).name == "Arena")
			{
				StartGame();
			}
		}
		//timeManager.StartTimer(1);
	}

	// Start is called before the first frame update
	void Start()
    {
		Time.timeScale = 1;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
		uiHandler = this.uiHandler.GetComponent<UIHandler>();
		timeManager = this.timeManager.GetComponent<TimeManager>();
		audioManager = this.audioManager.GetComponent<AudioManager>();
		levelGenerator = GameObject.FindObjectOfType<LevelGenerator>().GetComponent<LevelGenerator>();
		bossManager = GameObject.FindObjectOfType<BossManager>();
		StartCoroutine(InitialLoad());
    }

	#endregion
}
