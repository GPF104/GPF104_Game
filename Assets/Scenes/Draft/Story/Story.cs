using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void NextSlide()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LastSlide()
    {
        SceneManager.LoadScene("Story");
    }
}
