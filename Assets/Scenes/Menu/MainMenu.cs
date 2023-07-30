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
        yield return new WaitForSeconds(2);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Arena", LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

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

    void Start()
	{
        StartCoroutine(Fade());
        StartCoroutine(BackgroundLoad());
	}
    // Call this method when the play button is clicked or when you want to activate the background scene.

    public void PlayGame()
    {
        // Activate the background scene by setting allowSceneActivation to true.
        Scene backgroundScene = SceneManager.GetSceneByName("Arena");
        if (backgroundScene.IsValid() && !backgroundScene.isLoaded)
        {
            SceneManager.SetActiveScene(backgroundScene);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
