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
            }
            if (this.CompareTag("Diamond"))
            {
                LevelStats.diamondCollected++;
                PlayerActions.speed += 2f;
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
