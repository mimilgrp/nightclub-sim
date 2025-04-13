using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public string customerTag = "Customer";

    public int maxCustomers = 5;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;

    public void StartSpawnCustomers()
    {
        Debug.Log("CustomerSpawner: Start spawn customers");
        StartCoroutine(SpawnCustomers());
    }

    public void KillCustomers()
    {
        Debug.Log("CustomerSpawner: Kill customers");
        StopAllCoroutines();

        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

        foreach (GameObject customer in customers)
        {
            Destroy(customer);
        }
    }

    IEnumerator SpawnCustomers()
    {
        int customerNumber = 0;

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
