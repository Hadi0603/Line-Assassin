using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    public Color[] enabledTextColour;
    public Color disabledTextColour;
    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 2);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            Text buttonText = levelButtons[i].GetComponentInChildren<Text>();
            if (i + 2 > levelReached)
            {
                levelButtons[i].interactable = false;
                buttonText.color = disabledTextColour;
            }
            else
            {
                levelButtons[i].interactable = true;
                buttonText.color = enabledTextColour[i];
            }
        }
    }
    public void Select(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
