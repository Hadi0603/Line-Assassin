using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float minZ; 
    public float maxZ; 

    void Update()
    {
        float targetZ = player.position.z + offset.z;
        targetZ = Mathf.Clamp(targetZ, minZ, maxZ);
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, targetZ);
        transform.position = targetPosition;
    }
}
