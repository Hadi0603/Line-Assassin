using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public float minimumDistanceToFollow = 0.1f;
    public float distance = 0;
    public Transform playerTransform = null;
    public float speed = 10f;
    private bool isCollidingWithWall = false;
    public static bool isRunning = false;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;
    public static bool isAttacking = false;
    private Vector3 pos;
    public AudioSource attackSound;
    public void Update()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(playerTransform.position, attackRange, enemyLayer);
        if (enemiesInRange.Length > 0)
        {
            Attack(enemiesInRange[0].transform);
        }
        if (distance < minimumDistanceToFollow)
        {
            isRunning = false;
        }
    }
    public void MoveTowardsPath(List<Vector3> list, float time)
    {
        StartCoroutine(DelayMoveTowardsPath(list, time));
    }
    IEnumerator DelayMoveTowardsPath(List<Vector3> list, float time)
    {
        isRunning = false;
        isCollidingWithWall = false;
        yield return new WaitForSeconds(time);
        for (int i = 0; i < list.Count; i++)
        {
            distance = Vector3.Distance(playerTransform.position, list[i]);
            while (distance > minimumDistanceToFollow)
            {
                if (isCollidingWithWall)
                {
                    isRunning = false;
                    yield return null;
                    continue;
                }
                pos = Vector3.MoveTowards(playerTransform.position, list[i], speed * Time.deltaTime);
                if (pos.y < 0.5f)
                {
                    pos.y = 0.5f;
                }

                playerTransform.LookAt(pos);
                isRunning = true;
                playerTransform.position = pos;
                distance = Vector3.Distance(playerTransform.position, list[i]);

                // Check for enemies in attack range and attack if found
                Collider[] enemiesInRange = Physics.OverlapSphere(playerTransform.position, attackRange, enemyLayer);
                if (enemiesInRange.Length > 0)
                {
                    Attack(enemiesInRange[0].transform);
                }

                yield return null;
            }
        }
    }
    private void Attack(Transform enemy)
    {
        if (isAttacking) return;
        attackSound.Play();
        isRunning = false;
        isAttacking = true;

        // Attack logic here, for example, reducing enemy health
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }

        // You can add a delay here to simulate attack duration
        StartCoroutine(AttackCooldown());
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.1f); // Adjust cooldown as needed
        isAttacking = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isCollidingWithWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            isCollidingWithWall = false;
        }
    }
    
}