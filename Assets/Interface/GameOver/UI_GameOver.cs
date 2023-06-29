using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    GameManager gameManager;
    public void Replay()
	{
        Debug.Log("Play again");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
    public void Quit()
	{
        Debug.Log("Quit");
        Application.Quit();
	}
}
