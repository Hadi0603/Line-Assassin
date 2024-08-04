using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    private EnemyHealth enemy;
    private void Start()
    {
        enemy = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        healthBar.fillAmount = enemy.health / 600;
    }
}
