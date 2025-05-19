using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public float minRotationSpeed = 80f;
    public float maxRotationSpeed = 100f;

    private float rotationSpeed;

    void Start()
    {
        float randomAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);
    }
}
