using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour
{
    public Animator fridgeAnimator;
    private bool isAnimating = false;

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

    public void Interact(BeverageItem carriedItem)
    {

        if (carriedItem != null && System.Enum.TryParse<Beverage>(carriedItem.tag, out _))
        {
            string itemTag = carriedItem.tag;
            int itemQuantity = carriedItem.quantity;

            if (string.IsNullOrEmpty(itemTag))
            {
                Debug.LogWarning("StorageItem: item has no tag");
                return;
            }

            if (TotalStock + itemQuantity > stockCapacity)
            {
                Debug.Log($"StorageItem: insufficient capacity ({TotalStock + itemQuantity} > {stockCapacity})");
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
            Debug.Log($"StorageItem: {itemQuantity} {itemTag} added, item stock: {stockByType[itemTag]}/{stockCapacity}, total stock: {TotalStock}");
            if (!isAnimating && fridgeAnimator != null)
            {
                StartCoroutine(PlayFridgeAnimation());
            }
            else
            {
                Debug.Log("Fridge Animation Condition not verified");
            }
        }
        else
        {
            Debug.LogWarning("StorageItem: item has unauthorized tag");
            return;
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
        if (!isAnimating && fridgeAnimator != null)
        {
            StartCoroutine(PlayFridgeAnimation());
        }
        else
        {
            Debug.Log("Fridge Animation Condition not verified");
        }
    }
    IEnumerator PlayFridgeAnimation()
    {
        isAnimating = true;
        fridgeAnimator.Play("fridgeAnimation", 0, 0f);
        yield return new WaitForSeconds(2f);
        isAnimating = false;
    }

}