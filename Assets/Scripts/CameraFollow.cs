using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  
    private Vector3 initialOffset;

    void Start()
    {
        initialOffset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.z = player.position.z + initialOffset.z;
        transform.position = newPos;
    }
}
