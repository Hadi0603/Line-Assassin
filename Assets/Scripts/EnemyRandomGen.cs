using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomGen : MonoBehaviour
{
    public static Vector3 RandomPath(Vector3 startPoint, float radius)
    {
        Vector3 direction = Random.insideUnitSphere * radius;
        direction += startPoint;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(direction, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }
}