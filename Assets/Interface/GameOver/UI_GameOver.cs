using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_GameOver : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] TMP_Text tmpText;
    public void SetText(string text)
	{
		tmpText.text = "Game Over \n" + text;
	}
    public void Replay()
	{
        Debug.Log("Play again");
        SceneManager.LoadScene("Arena");
	}
    public void Quit()
	{
        Debug.Log("Quit");
		SceneManager.LoadScene(1);
	}
}
