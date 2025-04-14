using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoneyManager: MonoBehaviour
{
    public float money = 1000;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        DisplayMoney();
    }

    public static MoneyManager Instance { get; private set; }

    public void IncreaseMoney(float amount)
    {
        money += amount;
    }

    public void DecreaseMoney(float amount)
    {
        money -= amount;
    }

    public bool HasEnoughMoney(float amount)
    {
        return (money >= amount);
    }

    public void DisplayMoney()
    {
        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetMoney(money);
        }
    }
}
