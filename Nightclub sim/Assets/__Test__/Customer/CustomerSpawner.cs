using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject CustomerPrefab;

    public int maxCustomers = 5;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;

    private int customerNumber = 0;

    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    IEnumerator SpawnCustomers()
    {
        while (customerNumber < maxCustomers)
        {
            float randomDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomDelay);

            // Spawn the customer
            Instantiate(CustomerPrefab, transform.position, transform.rotation);
            customerNumber++;
        }

    }
}
