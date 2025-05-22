using UnityEngine;

public class InteractionRotation : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
