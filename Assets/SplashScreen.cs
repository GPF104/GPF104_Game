using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    bool isLoaded = false;
    bool isSkipping = false;
    [SerializeField] GameObject eventListener;
    IEnumerator Wait()
	{
        yield return new WaitForSeconds(0.25f);
        GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeIn();
        yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().fadeTime-0.75f);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(0);
    }
    IEnumerator Skip()
    {
        StopCoroutine(Wait());
        Destroy(eventListener);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            // Optional: You can display a progress bar or loading text here
            // to show the loading progress.

            // Check if the scene is fully loaded.
            if (asyncLoad.progress >= 1.0f && asyncLoad.isDone)
            {
                // The scene is fully loaded.
                Debug.Log("Loaded Scene in background");
                SceneManager.UnloadSceneAsync("SplashScreen");
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main Menu"));
                break;
            }
            isLoaded = true;
            yield return null;
        }
    }
    IEnumerator BackgroundLoad()
    {
        yield return new WaitForSeconds(2.5f);
        if (!SceneManager.GetSceneByName("Arena").isLoaded)
        {
            Debug.Log("Loading Scene async in background");
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
                isLoaded = true;
                yield return null;
            }
            if (!isSkipping)
			{
                // Find the Arena scene and mark it as DontDestroyOnLoad
                Debug.Log("Finished Loading");
                StartCoroutine(Wait());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeOut();
        StartCoroutine(BackgroundLoad());
    }
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            Debug.Log("Skip");
            if (isLoaded)
			{
                isSkipping = true;
                StartCoroutine(Skip());
                Debug.Log("Actually skipping");
            }

        }
	}

}
