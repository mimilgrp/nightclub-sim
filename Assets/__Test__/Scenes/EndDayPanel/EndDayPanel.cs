using TMPro;
using UnityEngine;

public class EndDayPanel : MonoBehaviour
{
    public TextMeshProUGUI dayText;

    public TextMeshProUGUI drinksPurchasedText;
    public TextMeshProUGUI drinksSoldText;
    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI customersText;
    public TextMeshProUGUI popularityText;
    public TextMeshProUGUI experienceText;

    public TextMeshProUGUI levelText;

    void Start()
    {
        int day = DayManager.Instance.day;

        dayText.text = $"End day {day}";

        float drinksPurchased = DayManager.Instance.drinksPurchased;
        float drinksSold = DayManager.Instance.drinksSold;
        float money = DayManager.Instance.money;

        drinksPurchasedText.text = $"{drinksPurchased:+$0.00;-$0.00;-$0.00}";
        drinksSoldText.text = $"{drinksSold:+$0.00;-$0.00;+$0.00}";
        moneyText.text = $"{money:+$0.00;-$0.00;+$0.00}";

        int customers = DayManager.Instance.customers;
        float popularity = DayManager.Instance.popularity;
        float experience = DayManager.Instance.experience;

        customersText.text = $"{customers}";
        popularityText.text = $"{popularity:+0;-0;+0}%";
        experienceText.text = $"{experience:+0;-0;+0}";

        int level = DayManager.Instance.level;

        if (level > 0)
        {
            levelText.text = $"Level {level} earned";
        }
        else
        {
            levelText.text = "No level earned";
        }
    }

    public void ClosePanel()
    {
        DayManager.Instance.ClosePanel();
    }
}
