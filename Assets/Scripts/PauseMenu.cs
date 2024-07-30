using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
