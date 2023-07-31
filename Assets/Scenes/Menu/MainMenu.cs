using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneFader fader;
	[SerializeField] EventSystem eventSystem;
	[SerializeField] GameObject musicPlayer;
    GameObject musicPlayerPrefab;
    IEnumerator Fade()
	{
        yield return new WaitForSeconds(fader.fadeTime);
        fader.FadeOut();

    }
    void Initialize()
	{
        eventSystem.enabled = true;
        Debug.Log("Number of AudioListeners: " + GameObject.FindGameObjectsWithTag("Music").Length);
        if (GameObject.FindGameObjectsWithTag("Music").Length == 0)
		{
            musicPlayerPrefab = Instantiate(musicPlayer);
        }
    }
    IEnumerator BackgroundLoad()
    {
        if (!SceneManager.GetSceneByName("Arena").isLoaded)
		{
            Debug.Log("Loading Scene async in background");
            yield return new WaitForSeconds(2);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

            // Wait until the scene is fully loaded
            while (!asyncLoad.isDone)
            {
                // Optional: You can display a progress bar or loading text here
                // to show the loading progress.

                // Check if the scene is fully loaded.
                if (asyncLoad.progress >= 1.0f && asyncLoad.isDone)
                {
                    // The scene is fully loaded.
                    Debug.Log("Loaded Scene in background");
                    break;
                }

                yield return null;
            }
        }
    }

	private void Awake()
	{
        StartCoroutine(BackgroundLoad());
    }
	void Start()
	{
        Initialize();
        Time.timeScale = 1.0f;
        StartCoroutine(Fade());
	}
    // Call this method when the play button is clicked or when you want to activate the background scene.

    IEnumerator FadeOutPlay(Scene scene)
	{
        fader.FadeIn();
        // Ensure only 1 audiolistener active.
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Music"))
        {
            if (gameObject.scene.name != "Arena")
            {
                Destroy(gameObject);
            }
        }
        yield return new WaitForSeconds(fader.fadeTime);
        

        Debug.Log("PLAY GAME");
        // Unload the current menu scene.
        SceneManager.UnloadSceneAsync("Main Menu");

        // Activate the background scene by setting allowSceneActivation to true.
        SceneManager.SetActiveScene(scene);

        // Call the StartGame method on the GameManager in the background scene.

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartGame();
    }
    public void PlayGame()
    {
        // Check if the background scene is already loaded.
        Scene backgroundScene = SceneManager.GetSceneByName("Arena");
        if (backgroundScene.IsValid() && backgroundScene.isLoaded)
        {
            StartCoroutine(FadeOutPlay(backgroundScene));

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
