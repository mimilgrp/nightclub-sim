using TMPro;
using UnityEngine;

public class EndDayPanel : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI purchasedText;
    public TextMeshProUGUI soldText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI customersText;
    public TextMeshProUGUI popularityText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI   levelText;

    void Start()
    {
        dayText.text = $"End day {DayManager.Instance.DayNumber}";

        purchasedText.text = DayManager.Instance.DrinksPurchased.ToString("+$0.00;-$0.00;$0.00");
        soldText.text = DayManager.Instance.DrinksSold.ToString("+$0.00;-$0.00;$0.00");
        moneyText.text = DayManager.Instance.MoneyEarned.ToString("+$0.00;-$0.00;$0.00");

        customersText.text = DayManager.Instance.CustomerVisits.ToString();
        popularityText.text = DayManager.Instance.PopularityEarned.ToString("+0;-0;0");
        experienceText.text = DayManager.Instance.ExperienceEarned.ToString("+0");

        if (DayManager.Instance.LevelEarned > 0)
            levelText.text = $"Level {DayManager.Instance.LevelEarned} earned";
        else
            levelText.text = "No level earned";
    }

    public void ClosePanel()
    {
        DayManager.Instance.ClosePanel();
    }
}
