using UnityEngine;

public class ShopSubPanel : MonoBehaviour
{
    public GameObject shopSubPanel;

    private Transform shopItemsSpaw;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("ShopItemsSpawn") != null)
            shopItemsSpaw = GameObject.FindGameObjectWithTag("ShopItemsSpawn").transform;

        shopSubPanel.SetActive(false);
    }

    public void OpenShopSubPanel()
    {
        shopSubPanel.SetActive(true);
    }

    public void ExitShopSubPanel()
    {
        shopSubPanel.SetActive(false);
    }

    public void BuyItem(GameObject itemPrefab)
    {       
        if (MoneyManager.Instance != null)
        {
            if (itemPrefab.GetComponent<TakeDropItem>() != null)
            {
                TakeDropItem item = itemPrefab.GetComponent<TakeDropItem>();
                float price = item.price;

                if (MoneyManager.Instance.HasEnoughMoney(price))
                {
                    MoneyManager.Instance.IncreaseMoney(-price);
                    DayManager.Instance.AddDrinksPurchased(price);
                    SpawnItem(itemPrefab);
                }
                else
                {
                    Debug.Log("ShopSubPanel: Not enough money");
                }
            }
        }
    }

    private void SpawnItem(GameObject itemPrefab)
    {
        Instantiate(itemPrefab, shopItemsSpaw.position, shopItemsSpaw.rotation);
    }
}
