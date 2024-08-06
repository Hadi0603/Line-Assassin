using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public float minimumDistanceToFollow = 0.1f;
    public float distance = 0;
    public Transform playerTransform = null;
    public float startSpeed = 10f;
    public static float speed;
    private bool isCollidingWithWall = false;
    public static bool isRunning = false;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;
    public static bool isAttacking = false;
    private Vector3 pos;
    public AudioSource attackSound;
    private void Start()
    {
        speed = startSpeed;
    }
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
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(attackDamage);
        }

        StartCoroutine(AttackCooldown());
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
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