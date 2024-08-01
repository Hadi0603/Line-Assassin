using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    void Update()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
