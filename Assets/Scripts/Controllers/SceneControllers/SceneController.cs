using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] SceneFader fader;

    int previousScene = -1;
    int currentScene = -1;
    IEnumerator LoadSequence(int scene)
	{
        fader.FadeIn();
        yield return new WaitForSeconds(fader.fadeTime);

        if (previousScene != -1)
		{
            SceneManager.UnloadSceneAsync(previousScene);
		}
        SceneManager.LoadSceneAsync(currentScene);
        fader.FadeOut();
	}
    public void LoadScene(int scene)
    {
        previousScene = currentScene;
        currentScene = scene;
        StartCoroutine(LoadSequence(scene));
    }
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSequence(0));
    }


}
