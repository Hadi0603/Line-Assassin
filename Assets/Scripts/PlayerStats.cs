using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Transform player;
    private float startHealth = 100f;
    public static float health;
    private bool isDead = false;
    private void Start()
    {
        health = startHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}