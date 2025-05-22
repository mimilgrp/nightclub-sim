using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDDisplay : MonoBehaviour
{
    [Header("Money")]
    public TextMeshProUGUI moneyText;

    [Header("Popularity")]
    public Image popularityBar;
    public TextMeshProUGUI popularityText;

    [Header("Experience")]
    public Image experienceBar;
    public TextMeshProUGUI levelText;

    [Header("Time")]
    public TextMeshProUGUI timeText;

    void Update()
    {
        float money = MoneyManager.Instance.money;
        float popularity = PopularityManager.Instance.popularity;
        float experience = ExperienceManager.Instance.experience;
        float experienceStep = ExperienceManager.Instance.experienceStep;
        int level = ExperienceManager.Instance.level;
        float time = TimeManager.Instance.gameTime;
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);

        moneyText.text = $"${money:0.00}";
        popularityBar.fillAmount = popularity / 100;
        popularityText.text = $"{popularity}%";
        experienceBar.fillAmount = experience / experienceStep;
        levelText.text = $"{level}";
        timeText.text = $"{hours:00}:{minutes:00}";
    }
}
