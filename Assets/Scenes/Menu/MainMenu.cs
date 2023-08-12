using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneFader fader;
	[SerializeField] EventSystem eventSystem;
	[SerializeField] GameObject musicPlayer;
    [SerializeField] Image overlayImage;
    public float fadeDuration = 1.0f; // Time taken for one fade cycle
    GameObject musicPlayerPrefab;
    IEnumerator Fade()
	{
        yield return new WaitForSeconds(fader.fadeTime);
        fader.FadeOut();

    }
    IEnumerator MenuAnimation()
    {
        while (true)
        {
            fadeDuration = Random.Range(1f, 2f);
            float elapsedTime = 0;

            // Fade from 0 to 1
            while (elapsedTime < fadeDuration)
            {
                float normalizedTime = elapsedTime / fadeDuration;
                Color color = overlayImage.color;
                color.a = Mathf.Lerp(0, 1, normalizedTime);
                overlayImage.color = color;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Wait for a delay
            yield return new WaitForSeconds(2);

            elapsedTime = 0;

            // Fade from 1 to 0
            while (elapsedTime < fadeDuration)
            {
                float normalizedTime = elapsedTime / fadeDuration;
                Color color = overlayImage.color;
                color.a = Mathf.Lerp(1, 0, normalizedTime);
                overlayImage.color = color;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Wait for a delay before repeating
            yield return new WaitForSeconds(2);
        }
    }
void Initialize()
	{
        if (SceneManager.GetSceneByName("SplashScreen").isLoaded)
		{
            SceneManager.UnloadSceneAsync("SplashScreen");
        }

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
        StartCoroutine(MenuAnimation());

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

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadStory()
    {
        SceneManager.LoadScene("Story");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
