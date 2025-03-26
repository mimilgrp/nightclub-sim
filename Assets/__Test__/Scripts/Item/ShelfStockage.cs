using UnityEngine;

public class ShelfStockage : MonoBehaviour
{
    [Header("Shelf Parameters")]
    public int stockAmount = 0;
    public string requiredTag = "";

    public void Interact(GameObject playerObject)
    {
        InteractableItem carriedItem = playerObject.GetComponentInChildren<InteractableItem>();

        if (carriedItem == null)
        {
            Debug.Log("Shelf: The player is not carrying any item.");
            return;
        }

        if (carriedItem.CompareTag(requiredTag))
        {
            stockAmount++;
            Debug.Log("Shelf: Item stored. New stock amount = " + stockAmount);

            Destroy(carriedItem.gameObject);
        }
        else
        {
            Debug.Log("Shelf: Player is carrying an item, but it's not the correct tag.");
        }
    }
}
