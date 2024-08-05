using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text starCount;
    [SerializeField] Text diamondCount;
    public static int star;
    public static int diamond;
    private void Start()
    {
        star = PlayerPrefs.GetInt("star", 0);
        diamond = PlayerPrefs.GetInt("diamond", 0);
    }
    private void Update()
    {
        starCount.text = star.ToString();
        diamondCount.text = diamond.ToString();
    }
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
        PlayerPrefs.SetInt("star", star);
        PlayerPrefs.SetInt("diamond", diamond);
        PlayerPrefs.Save();
    }
}
