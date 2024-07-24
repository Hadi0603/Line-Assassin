using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    [Header("Player to Target")]
    [SerializeField] public Transform player;

    [Header("Distances")]
    [SerializeField] float chaseRange = 8;
    [SerializeField] float randomPointRadius = 20;
    [SerializeField] float notificationRange = 12f;
    [SerializeField] float viewDistance = 15f; // Set a fixed view distance

    [Header("Speeds")]
    [SerializeField] float engageSpeed = 5;
    [SerializeField] float randomSpeed = 4;

    [Header("Relation with Player")]
    [SerializeField] bool canSeePlayer;
    [SerializeField] bool hasSeen;
    [SerializeField] bool hasDied;

    [Header("Attacking")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Misc")]
    [SerializeField] bool goingRandom;
    [SerializeField] bool takeNewPath;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;
    [SerializeField] float fovAngle = 90f; // Set a fixed field of view angle

    float distanceToTarget = Mathf.Infinity;

    NavMeshAgent navMeshAgent;
    Vector3 newPos;

    string currentSceneName;

    static List<EnemyMovement> allEnemies = new List<EnemyMovement>();
    bool notifiedNearbyEnemies = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        canSeePlayer = false;
        currentSceneName = SceneManager.GetActiveScene().name;

        newPos = transform.position;

        allEnemies.Add(this);
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(player.transform.position, transform.position);

        if (canSeePlayer && !hasDied) { hasSeen = true; }

        if (distanceToTarget > chaseRange) { hasSeen = false; }

        if (hasSeen)
        {
            EngageOnPlayer();

            if (!notifiedNearbyEnemies && distanceToTarget <= navMeshAgent.stoppingDistance)
            {
                NotifyNearbyEnemies();
                notifiedNearbyEnemies = true;
            }
        }
        else
        {
            // Reset notification flag when player is not seen
            notifiedNearbyEnemies = false;
        }

        if (!hasSeen)
        {
            if (goingRandom)
            {
                TakeRandomPath();
                if (takeNewPath == true)
                {
                    newPos = transform.position;
                    takeNewPath = false;
                }
            }
            else
                StayIdle();
        }

        if (hasDied) { navMeshAgent.SetDestination(transform.position); }
    }

    private void FixedUpdate()
    {
        CreateFOV();
    }

    void NotifyNearbyEnemies()
    {
        List<EnemyMovement> enemiesToNotify = new List<EnemyMovement>(allEnemies);

        // Find nearby enemies to notify
        foreach (EnemyMovement enemy in enemiesToNotify)
        {
            if (enemy == null || enemy == this || enemy.notifiedNearbyEnemies)
                continue;

            float distanceToOtherEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToOtherEnemy <= chaseRange)
            {
                enemy.StartChasing();
            }
        }

        // Mark this enemy as notified
        notifiedNearbyEnemies = true;
    }

    void StartChasing()
    {
        hasSeen = true;
    }

    private void StayIdle()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    private void TakeRandomPath()
    {
        if (Vector3.Distance(newPos, transform.position) <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.speed = randomSpeed;
            newPos = EnemyRandomGen.RandomPath(transform.position, randomPointRadius);
            navMeshAgent.SetDestination(newPos);
        }
    }

    private void EngageOnPlayer()
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChasePlayer();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            if (fireCountDown <= 0f && PlayerActions.isAttacking == false)
            {
                AttackPlayer();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
        takeNewPath = true;
    }

    public void ChasePlayer()
    {
        navMeshAgent.speed = engageSpeed;
        navMeshAgent.SetDestination(player.transform.position);
        hasSeen = true;
    }

    private void AttackPlayer()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(player);
        }
        FaceTarget();
        navMeshAgent.SetDestination(transform.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
    }

    IEnumerator FOV()
    {
        float delay = 0.2f;
        while (true)
        {
            yield return new WaitForSeconds(delay);
            CreateFOV();
        }
    }

    private void CreateFOV()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < fovAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, direction, distance, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Max View Distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.DrawLine(transform.position, newPos);

        // Max Chase Distance
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Can See Player
        if (canSeePlayer)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}