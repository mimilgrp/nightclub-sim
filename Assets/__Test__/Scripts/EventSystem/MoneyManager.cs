using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoneyManager: MonoBehaviour
{
    public float money = 0;

    public void IncreaseMoney(int amount)
    {
        money += amount;

        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetMoney(money);
        }
    }

    public bool HasEnoughMoney(int amount)
    {
        return (money >= amount);
    }
}
