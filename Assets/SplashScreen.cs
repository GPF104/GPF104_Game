using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float interval = 8;
    IEnumerator Wait()
	{
        yield return new WaitForSeconds(interval);
        GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeIn();

        SceneManager.LoadSceneAsync(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Fader").GetComponent<SceneFader>().FadeOut();
        StartCoroutine(Wait());
    }
	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
            StopCoroutine(Wait());
            SceneManager.LoadSceneAsync(1);
        }
	}

}
