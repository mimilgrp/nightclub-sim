using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 minXZ;
    public Vector2 maxXZ;
    public float followSpeed = 10f;
    public float height = 117f;

    private Vector3 offset;

    private void Start()
    {
        // Calculate the offset based on initial positions
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Desired position based on offset
        Vector3 desiredPosition = target.position + offset;

        // Clamp X and Z
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minXZ.x, maxXZ.x);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, minXZ.y, maxXZ.y);

        // Keep Y fixed
        desiredPosition.y = height;

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
