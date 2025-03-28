using TMPro;
using UnityEngine;

public class MoneyHUD : MonoBehaviour
{
    private int moneyAmount;
    TextMeshProUGUI moneyshowed;
    void Start()
    {
        moneyAmount = 0;
        moneyshowed = GetComponent<TextMeshProUGUI>();
    }
    public void addMoney(int m)
    {
        moneyAmount += m;
        moneyshowed.text = moneyAmount.ToString();
    }

    public void removeMoney(int m)
    {
        moneyAmount -= m;
        moneyshowed.text = moneyAmount.ToString();
    }
}
