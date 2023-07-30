using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    bool isBackgroundSceneLoaded = false;
    IEnumerator Fade()
	{
        yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().fadeTime);
        GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeOut();
    }

    IEnumerator BackgroundLoad()
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
                isBackgroundSceneLoaded = true;
                Debug.Log("Loaded Scene in background");
                break;
            }

            yield return null;
        }
    }

	private void Awake()
	{
        StartCoroutine(BackgroundLoad());
    }
	void Start()
	{
        StartCoroutine(Fade());
	}
    // Call this method when the play button is clicked or when you want to activate the background scene.

    public void PlayGame()
    {
        // Check if the background scene is already loaded.
        Scene backgroundScene = SceneManager.GetSceneByName("Arena");
        if (backgroundScene.IsValid() && backgroundScene.isLoaded)
        {
            Debug.Log("PLAY GAME");
            // Unload the current menu scene.
            SceneManager.UnloadSceneAsync("Main Menu");

            // Activate the background scene by setting allowSceneActivation to true.
            SceneManager.SetActiveScene(backgroundScene);

            // Call the StartGame method on the GameManager in the background scene.
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartGame();

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
