using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        SaveProgress();
        Application.Quit();
    }
    public void SaveProgress()
    {
        PlayerPrefs.Save();
    }
}
