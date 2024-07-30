using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStats : MonoBehaviour
{
    [SerializeField] Text enemyCount;
    [SerializeField] int totalEnemy;
    [SerializeField] Text healthCount;
    [SerializeField] Text starCount;
    [SerializeField] int totalStar;
    [SerializeField] GameObject levelCompleteUI;
    public static int enemiesKilled = 0;
    public static int starCollected = 0;
    private void Start()
    {
        enemiesKilled = 0;
        starCollected = 0;
        levelCompleteUI.SetActive(false);
        Time.timeScale = 1f;
    }
    void Update()
    {
        enemyCount.text = enemiesKilled.ToString() + "/" + totalEnemy.ToString();
        healthCount.text = PlayerStats.health.ToString();
        starCount.text = starCollected.ToString() + "/" + totalStar.ToString();
        if (enemiesKilled == totalEnemy)
        {
            levelCompleteUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            levelCompleteUI.SetActive(false);
        }
    }
}
