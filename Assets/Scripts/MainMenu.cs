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
    private void Update()
    {
        starCount.text = star.ToString();
        diamondCount.text = diamond.ToString();
    }
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
