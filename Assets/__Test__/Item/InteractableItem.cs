using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    private bool isCarried = false;
    public Transform carryPoint;
    public virtual void Interaction()
    {

        Debug.Log("Interacted with item: " + gameObject.name);

        if (!isCarried)
        {
            // pick up
            Debug.Log("Picking up item: " + gameObject.name);

            // Make this item a child of the carryPoint
            transform.SetParent(carryPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isCarried = true;
        }
        else
        {
            // DROP
            Debug.Log("Dropping item: " + gameObject.name);

            // Detach from the carryPoint
            transform.SetParent(null);
            isCarried = false;
        }
    }
}
