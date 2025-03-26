using UnityEngine;

public class PurchaseSubPanel : MonoBehaviour
{
    public GameObject purchaseSubPanel;
    public GameObject beveragesPrefab;

    private Transform beveragesSpawn;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("BeveragesSpawn") != null)
            beveragesSpawn = GameObject.FindGameObjectWithTag("BeveragesSpawn").transform;

        purchaseSubPanel.SetActive(false);
    }

    public void OpenPurchaseSubPanel()
    {
        purchaseSubPanel.SetActive(true);
    }

    public void ExitPurchaseSubPanel()
    {
        purchaseSubPanel.SetActive(false);
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
