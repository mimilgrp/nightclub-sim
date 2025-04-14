using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoneyManager: MonoBehaviour
{
    public float money = 0;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static MoneyManager Instance { get; private set; }

    public void IncreaseMoney(float amount)
    {
        money += amount;

        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetMoney(money);
        }
    }

    public void DecreaseMoney(float amount)
    {
        money -= amount;

        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetMoney(money);
        }
    }

    public bool HasEnoughMoney(float amount)
    {
        return (money >= amount);
    }
}
