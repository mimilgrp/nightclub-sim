using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public virtual void Interaction()
    {
        Debug.Log("Interacted with item: " + gameObject.name);
        // pick it up, open a door, etc.
    }
}
