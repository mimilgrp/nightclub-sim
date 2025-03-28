using UnityEngine;

public class StorageItem : MonoBehaviour
{
    [Header("Storage Parameters")]
    public int stockAmount = 0;
    public string requiredTag = "";

    public void Interact(TakeDropItem carriedItem)
    {
        if (!carriedItem.CompareTag(requiredTag))
        {
            Debug.Log("Shelf: Incorrect item tag");
        }
        else
        {
            stockAmount += carriedItem.itemQuantity;
            Destroy(carriedItem.gameObject);

            Debug.Log("Shelf: Item added, new stock amount: " + stockAmount);
        }
    }
}
