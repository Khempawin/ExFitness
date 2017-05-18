using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("gameScene0");
    }

    public void SelectProfile()
    {
        SceneManager.LoadScene("selectProfile");
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("highScore");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
