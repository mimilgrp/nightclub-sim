using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 minXZ;
    public Vector2 maxXZ;
    public float followSpeed = 10f;
    public float height = 117f;
    public float ratioFOV = 0.35f;

    private Camera mainCam;
    private Vector3 offset;

    private void Start()
    {
        mainCam = Camera.main;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        
        float vFOV = mainCam.fieldOfView;
        float aspect = mainCam.aspect;

        float halfHeight = Mathf.Tan(vFOV * ratioFOV * Mathf.Deg2Rad) * height;
        float halfWidth = halfHeight * aspect;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minXZ.x + halfWidth, maxXZ.x - halfWidth);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, minXZ.y + halfHeight, maxXZ.y - halfHeight);
        desiredPosition.y = height;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
