using UnityEngine;

public class DailyFlow : MonoBehaviour
{
    public GameObject customerSpawnerGameObject;

    private CustomerSpawner customerSpawner;

    void Start()
    {
        customerSpawner = customerSpawnerGameObject.GetComponent<CustomerSpawner>();

        if (customerSpawner == null)
        {
            Debug.Log("DailyFlow: CustomerSpawner not found");
        }
    }

    public void Preparation()
    {
        Debug.Log("DailyFlow: Preparation");
    }

    public void Showing()
    {
        Debug.Log("DailyFlow: Showing");
        customerSpawner.StartSpawnCustomers();
    }

    public void Closing()
    {
        Debug.Log("DailyFlow: Closing");
        customerSpawner.KillCustomers();
    }

    public enum Shift
    {
        Preparation,
        Showing,
        Closing
    }
}
