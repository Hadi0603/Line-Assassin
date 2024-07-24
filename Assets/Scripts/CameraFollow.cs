using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    void Update()
    {
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);
        transform.position = targetPosition;
    }
}
