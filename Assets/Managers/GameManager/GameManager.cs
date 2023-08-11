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


	public LevelGenerator levelGenerator;
	#endregion

	#region Attributes

	EventSystemController eventSystemController;

	public int score = 0;
	public bool GameFinished = false;
	public bool GamePaused = false;
	private bool isInBackground = true;

	public float slowdownFactor = 0.05f; // Adjust this to control the rate of slowdown
	public float slowdownDuration = 2.0f;

	public void AddScore(int input)
	{
		score += input;
		uiHandler.uiScore.SetText(score.ToString());
	}

	// Gradually slwo the game down when it's game over.
	IEnumerator SlowDown()
	{
		float targetTimeScale = 0.0f;

		while (Time.timeScale > targetTimeScale)
		{
			Time.timeScale -= slowdownFactor * Time.deltaTime / slowdownDuration;
			Time.timeScale = Mathf.Clamp(Time.timeScale, targetTimeScale, 1.0f);

			yield return null; // Wait for the next frame
		}

		Time.timeScale = targetTimeScale; // Ensure timeScale reaches exactly 0
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
		StartCoroutine(SlowDown());
		GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeIn();
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
		mainCamera.GetComponent<MainCamera>().StartListening();
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

		if (SceneManager.sceneCount == 1)
		{
			if (SceneManager.GetSceneAt(0).name == "Arena")
			{
				StartGame();
			}
		}
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
