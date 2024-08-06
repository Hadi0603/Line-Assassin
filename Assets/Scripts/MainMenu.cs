using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    public void Play()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void Tutorial()
    {
        tutorial.SetActive(true);
    }
    public void cancel()
    {
        tutorial.SetActive(false);
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
