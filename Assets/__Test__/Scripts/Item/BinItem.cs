using UnityEngine;

public class BinItem : MonoBehaviour
{
    public void Interact(GameObject playerObject)
    {
        TakeDropItem carriedItem = playerObject.GetComponentInChildren<TakeDropItem>();

        if (carriedItem == null)
        {
            Debug.Log("Shelf: No item carried");
        }
        else
        {
            Debug.Log("Shelf: Item destroyed");
            Destroy(carriedItem.gameObject);
        }
    }
}
