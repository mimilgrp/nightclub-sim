using UnityEngine;

public class ShopSubPanel : MonoBehaviour
{
    public GameObject shopSubPanel;
    public GameObject beveragesPrefab;

    private Transform beveragesSpawn;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("BeveragesSpawn") != null)
            beveragesSpawn = GameObject.FindGameObjectWithTag("BeveragesSpawn").transform;

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

    public void BuyBeverages()
    {
        SpawnBeverages();
    }

    private void SpawnBeverages()
    {
        Instantiate(beveragesPrefab, beveragesSpawn.position, beveragesSpawn.rotation);
    }
}
