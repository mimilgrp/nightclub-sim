using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour
{
    public enum Beverage
    {
        Beer,
        Vodka,
        Tequila,
        Liquor
    }

    [Header("Storage Parameters")]
    public int stockCapacity = 120;

    private Dictionary<string, int> stockByType = new Dictionary<string, int>();

    private int TotalStock => GetTotalStock();

    public void Interact(TakeDropItem carriedItem)
    {

        if (carriedItem != null && System.Enum.TryParse<Beverage>(carriedItem.tag, out _))
        {
            string itemTag = carriedItem.tag;
            int itemQuantity = carriedItem.itemQuantity;

            if (string.IsNullOrEmpty(itemTag))
            {
                Debug.LogWarning("L'objet transport� n'a pas de tag !");
                return;
            }

            if (TotalStock + itemQuantity > stockCapacity)
            {
                Debug.Log($"Shelf: Capacit� insuffisante ({TotalStock + itemQuantity} > {stockCapacity})");
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
            Debug.Log($"Shelf: {itemQuantity} {itemTag} ajout�s. Stock actuel : {stockByType[itemTag]} ({TotalStock}/{stockCapacity})");
        }
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
    public int GetQuantity(string itemTag)
    {
        return stockByType.ContainsKey(itemTag) ? stockByType[itemTag] : 0;
    }
    public void RemoveItems(Dictionary<string, int> itemsToRemove)
    {
        foreach (var item in itemsToRemove)
        {
            if (stockByType.ContainsKey(item.Key))
            {
                stockByType[item.Key] -= item.Value;

                if (stockByType[item.Key] <= 0)
                {
                    stockByType.Remove(item.Key);
                }
            }
        }
    }
}
