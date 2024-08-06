using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] Button pauseBtn;
    private DrawLines drawLines;
    private void Start()
    {
        Time.timeScale = 0f;
        drawLines=FindAnyObjectByType<DrawLines>();
        tutorial.SetActive(true);
        drawLines.enabled = false;
        pauseBtn.interactable = false;
    }
    public void Cancel()
    {
        Time.timeScale = 1f;
        tutorial.SetActive(false);
        drawLines.enabled = true;
        pauseBtn.interactable = true;
    }
}
