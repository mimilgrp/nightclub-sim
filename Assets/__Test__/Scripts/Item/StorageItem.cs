using UnityEngine;

public class StorageItem : MonoBehaviour
{
    [Header("Storage Parameters")]
    public int stockAmount = 0;
    public int stockCapacity = 120;
    public string requiredTag = "";

    public void Interact(TakeDropItem carriedItem)
    {
        if (!carriedItem.CompareTag(requiredTag))
        {
            Debug.Log("Shelf: Incorrect item tag");
        }
        else
        {
            int itemQuantity = carriedItem.itemQuantity;

            if ((stockAmount + itemQuantity) > stockCapacity)
            {
                Debug.Log("Shelf: Insufficient capacity (" + (stockAmount + itemQuantity) + " > " + stockCapacity + ")");
            }
            else
            {
                stockAmount += itemQuantity;
                Destroy(carriedItem.gameObject);

                Debug.Log("Shelf: Item added (new stock amount: " + stockAmount + ")");
            }
        }
    }
}
