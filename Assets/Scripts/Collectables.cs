using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private bool isCollecting;
    void Start()
    {
        isCollecting = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.CompareTag("Star"))
            {
                LevelStats.starCollected++;
                MainMenu.star++;
                PlayerPrefs.SetInt("star", MainMenu.star);
            }
            if (this.CompareTag("Diamond"))
            {
                LevelStats.diamondCollected++;
                MainMenu.diamond++;
                PlayerPrefs.SetInt("diamond", MainMenu.diamond);
            }
            if (this.CompareTag("Heart"))
            {
                PlayerStats.health += 50;
            }
            isCollecting = true;
            SaveProgress();
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCollecting = false;
        }
    }
    private void SaveProgress()
    {
        PlayerPrefs.Save();
    }
}
