using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuyButton : MonoBehaviour
{
    public BeverageItem shopItem;

    private Button button;
    private TextMeshProUGUI buttonText;
    private GameObject itemSpawner;

    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        itemSpawner = GameObject.FindGameObjectWithTag("ItemSpawner");
        
        if (button != null)
        {
            button.onClick.AddListener(BuyShopItem);
        }

        if (buttonText != null)
        {
            if (shopItem != null)
            {
                buttonText.text = $"Buy x{shopItem.quantity} {shopItem.price:$0.##}";
            }
            else
            {
                buttonText.text = null;
            }
        }
    }

    public void BuyShopItem()
    {
        if (MoneyManager.Instance != null)
        {
            float price = shopItem.price;

            if (!MoneyManager.Instance.HasEnoughMoney(price))
            {
                HUDDisplay.Instance.ShowNotification("You do not have enough money to buy this");
            }
            else
            {
                MoneyManager.Instance.AddMoney(-price, DayManager.Transaction.DrinksPurchased);
                Instantiate(shopItem, itemSpawner.transform.position, itemSpawner.transform.rotation);
            }
        }
    }
}
