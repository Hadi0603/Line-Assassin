using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStats : MonoBehaviour
{
    [SerializeField] public Text enemyCount;
    [SerializeField] int totalEnemy;
    [SerializeField] public Text healthCount;
    [SerializeField] public Text starCount;
    [SerializeField] int totalStar;
    [SerializeField] int totalDiamond;
    [SerializeField] public Text diamondCount;
    [SerializeField] GameObject levelCompleteUI;
    [SerializeField] GameObject gameOverUI;
    public int levelToUnlock;
    public static int enemiesKilled;
    public static int starCollected;
    public static int diamondCollected;
    private void Start()
    {
        enemiesKilled = 0;
        starCollected = 0;
        diamondCollected= 0;
        levelCompleteUI.SetActive(false);
    }
    void Update()
    {
        enemyCount.text = enemiesKilled.ToString() + "/" + totalEnemy.ToString();
        healthCount.text = PlayerStats.health.ToString();
        starCount.text = starCollected.ToString() + "/" + totalStar.ToString();
        diamondCount.text=diamondCollected.ToString()+"/"+totalDiamond.ToString();
        if (PlayerStats.health <= 0)
        {
            gameOverUI.SetActive(true);
        }
        if (PlayerStats.health > 0)
        {
            gameOverUI.SetActive(false);
        }
        if (enemiesKilled == totalEnemy)
        {
            levelCompleteUI.SetActive(true);
            if (levelToUnlock > PlayerPrefs.GetInt("levelReached"))
            {
                PlayerPrefs.SetInt("levelReached", levelToUnlock);
            }
        }
        if (enemiesKilled < totalEnemy)
        {
            levelCompleteUI.SetActive(false);
        }
    }
}
