using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    private DrawLines drawLinesScript;
    private void Start()
    {
        drawLinesScript=FindAnyObjectByType<DrawLines>();
    }
    public void Pause()
    {
        if (drawLinesScript != null)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            drawLinesScript.enabled = false;
        }
    }
    public void Continue()
    {
        if (drawLinesScript != null)
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
            drawLinesScript.enabled = true;
        }
    }
    public void Retry()
    {
        if (drawLinesScript != null)
        {
            Time.timeScale = 1f;
            drawLinesScript.enabled = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        drawLinesScript.enabled = true;
        SceneManager.LoadScene("MainMenu");
    }
}
