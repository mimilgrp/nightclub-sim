using UnityEngine;

public class MoneyManager: MonoBehaviour
{
    public float money = 1000f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static MoneyManager Instance { get; private set; }

    public void AddMoney(float value, DayManager.Transaction transaction)
    {
        money += value;
        DayManager.Instance.AddMoney(value, transaction);
    }

    public bool HasEnoughMoney(float amount)
    {
        return (money >= amount);
    }
}
