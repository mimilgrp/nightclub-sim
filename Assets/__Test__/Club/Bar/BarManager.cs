using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    StorageItem fridge;
    void Start()
    {
        fridge = GetComponentInChildren<StorageItem>();
    }

    public bool canPrepareDrink(Dictionary<string, int>  requiredIngredients)
    {
        foreach (var item in requiredIngredients)
        {
            int availableQty = fridge.GetQuantity(item.Key);
            if (availableQty < item.Value)
            {
                Debug.Log($"Manque de {item.Key} ({availableQty}/{item.Value})");
                return false;
            }
        }
        return true;
    }

    public bool PrepareDrink()
    {
        Dictionary<string, int> requiredIngredients = new Dictionary<string, int>()
        {
            { "Beer", 1 }
        };
        if (!canPrepareDrink(requiredIngredients))
        {
            Debug.LogWarning("Ingrédients insuffisants pour préparer la boisson !");
            return false;
        }

        fridge.RemoveItems(requiredIngredients);
        Debug.LogWarning("Beverage served");
        return true;
    }
}
