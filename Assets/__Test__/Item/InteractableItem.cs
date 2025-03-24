using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    private bool isCarried = false;

    public Transform carryPoint;

    public virtual void Interaction(Transform player)
    {
        Debug.Log("Interacted with item: " + gameObject.name);
        if (carryPoint == null)
        {
            Debug.LogWarning("No carryPoint assigned");
        }

        if (!isCarried)
        {
            // pick up
            Debug.Log("Picking up item: " + gameObject.name);

            // Make this item a child of the carryPoint
            transform.SetParent(carryPoint);

            isCarried = true;
        }
        else
        {
            // DROP
            Debug.Log("Dropping item: " + gameObject.name);

            // Detach from the carryPoint
            transform.SetParent(null);

            // Re-enable physics
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }

            isCarried = false;
        }
    }
}
