using UnityEngine;

public class DailyFlow : MonoBehaviour
{
    public Shift GameShift;

    [Header("Time Shifts")]
    public int preparationTime = 57600;
    public int showingTime = 82800;
    public int closingTime = 14400;

    [Header("Time Scales")]
    public float preparationTimeScale = 180f;
    public float showingTimeScale = 120f;

    private GameObject customerSpawnerGameObject;
    private CustomerSpawner customerSpawner;

    public enum Shift
    {
        Preparation,
        Showing,
        Closing
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        customerSpawnerGameObject = GameObject.FindGameObjectWithTag("CustomerSpawner");

        if (customerSpawnerGameObject == null)
        {
            Debug.LogWarning("DailyFlow: CustomerSpawnerGameObject not found");
            return;
        }

        customerSpawner = customerSpawnerGameObject.GetComponent<CustomerSpawner>();

        if (customerSpawner == null)
        {
            Debug.LogWarning("DailyFlow: CustomerSpawner not found");
        }
    }

    public static DailyFlow Instance { get; private set; }

    public void Preparation()
    {
        GameShift = Shift.Preparation;
        TimeManager.Instance.gameTime = preparationTime;
        TimeManager.Instance.gameTimeScale = preparationTimeScale;
    }

    public void Showing()
    {
        GameShift = Shift.Showing;
        TimeManager.Instance.gameTime = showingTime;
        TimeManager.Instance.gameTimeScale = showingTimeScale;

        customerSpawner.StartSpawnCustomers();
    }

    public void Closing()
    {
        GameShift = Shift.Closing;
        TimeManager.Instance.gameTime = closingTime;
        TimeManager.Instance.gameTimeScale = 0f;

        customerSpawner.KillCustomers();
        DayManager.Instance.EndDay();
    }
}
