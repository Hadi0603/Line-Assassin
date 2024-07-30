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
            isCollecting = true;
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
}
