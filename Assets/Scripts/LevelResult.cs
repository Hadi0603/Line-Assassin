using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelResult : MonoBehaviour
{
    private LevelStats stats;
    [SerializeField] Text enemyText;
    [SerializeField] Text healthText;
    [SerializeField] Text starText;
    [SerializeField] Text diamondText;
    private void Update()
    {
        stats = GetComponent<LevelStats>();
        enemyText.text = stats.enemyCount.text;
        healthText.text = stats.healthCount.text;
        starText.text = stats.starCount.text;
        diamondText.text = stats.diamondCount.text;
    }
}
