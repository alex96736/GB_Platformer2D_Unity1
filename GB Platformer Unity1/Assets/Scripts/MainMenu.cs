using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Panel_Settings;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Settings()
    {
        Panel_Settings.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
