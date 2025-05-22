using System.Collections.Generic;
using UnityEngine;
public class BarManager : MonoBehaviour
{
    StorageItem fridge;
    private List<CustomerAI> waitingCustomers = new List<CustomerAI>();
    private int maxQueueSize = 3;

    void Start()
    {
        fridge = GetComponentInChildren<StorageItem>();
    }

    public bool canPrepareDrink(Dictionary<string, int> requiredIngredients)
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

    public bool RegisterCustomer(CustomerAI customer)
    {
        //Securité
        if (waitingCustomers.Contains(customer))
            return false;

        if (waitingCustomers.Count >= maxQueueSize)
        {
            Debug.Log("The queue is full");
            return false;
        }//Fin de Securité

        waitingCustomers.Add(customer);
        Debug.Log("Client add to the bar queue");
        return true;
    }
    public void UnregisterCustomer(CustomerAI customer)
    {
        if (waitingCustomers.Remove(customer))
        {
            Debug.Log($"Client retiré de la queue");
        }
    }

    public void ServeNextCustomer()
    {
        if (waitingCustomers.Count == 0)
        {
            Debug.Log("No client waiting to be served");
            return;
        }

        Dictionary<string, int> requiredIngredients = new Dictionary<string, int>()
        {
            { "Beer", 1 }
        };

        if (!canPrepareDrink(requiredIngredients))
        {
            Debug.LogWarning("Not enough ingredient");
            return;
        }

        fridge.RemoveItems(requiredIngredients);
        Debug.Log("Drink served");
        waitingCustomers[0].OnDrinkServed();
        waitingCustomers.RemoveAt(0);
    }
}