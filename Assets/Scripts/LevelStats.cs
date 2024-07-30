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
    [SerializeField] int totalDiamond;
    [SerializeField] Text diamondCount;
    [SerializeField] GameObject levelCompleteUI;
    public static int enemiesKilled;
    public static int starCollected;
    public static int diamondCollected;
    private void Start()
    {
        enemiesKilled = 0;
        starCollected = 0;
        diamondCollected= 0;
        levelCompleteUI.SetActive(false);
        Time.timeScale = 1f;
    }
    void Update()
    {
        enemyCount.text = enemiesKilled.ToString() + "/" + totalEnemy.ToString();
        LevelResult.enemyText.text = enemyCount.text;
        healthCount.text = PlayerStats.health.ToString();
        LevelResult.healthText.text = healthCount.text;
        starCount.text = starCollected.ToString() + "/" + totalStar.ToString();
        LevelResult.starText.text = starCollected.ToString();
        diamondCount.text=diamondCollected.ToString()+"/"+totalDiamond.ToString();
        LevelResult.diamondText.text = diamondCount.text;
        if (enemiesKilled == totalEnemy)
        {
            levelCompleteUI.SetActive(true);
        }
        else
        {
            levelCompleteUI.SetActive(false);
        }
    }
}
