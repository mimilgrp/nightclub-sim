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
        SpawnItem(itemPrefab);
    }

    private void SpawnItem(GameObject itemPrefab)
    {
        Instantiate(itemPrefab, shopItemsSpaw.position, shopItemsSpaw.rotation);
    }
}
