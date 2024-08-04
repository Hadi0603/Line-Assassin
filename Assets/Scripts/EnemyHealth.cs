using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    private EnemyMovement enemy;
    [SerializeField] GameObject attackEffect;
    private void Start()
    {
        enemy = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        enemy.NotifyNearbyEnemies();
        GameObject effectAtt = (GameObject)Instantiate(attackEffect, transform.position, transform.rotation);
        Destroy(effectAtt, 1f);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        LevelStats.enemiesKilled++;
        Destroy(gameObject);
    }
}
