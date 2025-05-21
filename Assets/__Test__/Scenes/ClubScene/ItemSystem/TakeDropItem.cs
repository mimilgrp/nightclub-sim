using UnityEngine;

public class TakeDropItem : MonoBehaviour
{
    private bool isCarried = false;

    public virtual void Interact(Transform carryPoint)
    {
        if (!isCarried)
        {
            transform.SetParent(carryPoint);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            isCarried = true;
        }
        else
        {
            transform.SetParent(null);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            isCarried = false;
        }
    }
}
