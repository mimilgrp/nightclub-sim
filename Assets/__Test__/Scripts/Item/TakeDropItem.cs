using UnityEngine;

public class TakeDropItem : MonoBehaviour
{
    public int itemQuantity;
    public float price;

    private bool isCarried = false;

    public virtual void Interact(Transform carryPoint)
    {
        if (!isCarried)
        {
            // Take object 
            Debug.Log("Take item: " + gameObject.name);

            // Make this object a child of the carryPoint
            transform.SetParent(carryPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            isCarried = true;
        }
        else
        {
            // Drop object
            Debug.Log("Drop item: " + gameObject.name);

            // Detach object from the carryPoint
            transform.SetParent(null);
            isCarried = false;
        }
    }
}
