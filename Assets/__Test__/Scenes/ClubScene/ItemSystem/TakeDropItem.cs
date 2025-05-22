using UnityEngine;

public class TakeDropItem : MonoBehaviour
{
    private bool isCarried = false;

    public void Take(Transform carryPoint)
    {
        if (!isCarried)
        {
            transform.SetParent(carryPoint);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            isCarried = true;
        }
    }

    public void Drop()
    {
        if (isCarried)
        {
            transform.SetParent(null);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            isCarried = false;
        }
    }
}
