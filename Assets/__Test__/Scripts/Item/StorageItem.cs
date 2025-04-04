using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour
{
    [Header("Storage Parameters")]
    public int stockCapacity = 120;

    private Dictionary<string, int> stockByType = new Dictionary<string, int>();

    private int TotalStock => GetTotalStock();

    public void Interact(TakeDropItem carriedItem)
    {
        string itemTag = carriedItem.tag;
        int itemQuantity = carriedItem.itemQuantity;

        if (string.IsNullOrEmpty(itemTag))
        {
            Debug.LogWarning("L'objet transporté n'a pas de tag !");
            return;
        }

        if (TotalStock + itemQuantity > stockCapacity)
        {
            Debug.Log($"Shelf: Capacité insuffisante ({TotalStock + itemQuantity} > {stockCapacity})");
            return;
        }

        if (stockByType.ContainsKey(itemTag))
        {
            stockByType[itemTag] += itemQuantity;
        }
        else
        {
            stockByType[itemTag] = itemQuantity;
        }

        Destroy(carriedItem.gameObject);
        Debug.Log($"Shelf: {itemQuantity} {itemTag} ajoutés. Stock actuel : {stockByType[itemTag]} ({TotalStock}/{stockCapacity})");

    }
    private int GetTotalStock()
    {
        int total = 0;
        foreach (int qty in stockByType.Values)
        {
            total += qty;
        }
        return total;
    }
}
