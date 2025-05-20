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

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        float vFOV = mainCam.fieldOfView;
        float aspect = mainCam.aspect;

        float halfHeight = Mathf.Tan(vFOV * ratioFOV * Mathf.Deg2Rad) * height;
        float halfWidth = halfHeight * aspect;

        float pitch = transform.eulerAngles.x;
        float radians = pitch * Mathf.Deg2Rad;

        float distanceZ = height / Mathf.Tan(radians);
        Vector3 offset = new Vector3(0, height, -distanceZ);

        Vector3 desiredPosition = target.position + offset;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minXZ.x + halfWidth, maxXZ.x - halfWidth);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, minXZ.y + halfHeight, maxXZ.y - halfHeight);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
