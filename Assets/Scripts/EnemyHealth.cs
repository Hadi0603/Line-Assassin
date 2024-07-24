using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    [SerializeField] GameObject attackEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameObject effectAtt = (GameObject)Instantiate(attackEffect, transform.position, transform.rotation);
        Destroy(effectAtt, 1f);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
