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

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        experienceBar.fillAmount = 0;
    }

    public static HUDDisplay Instance { get; private set; }

    public void SetMoney(float money)
    {
        moneyText.text = string.Format("${0:F2}", money);
    }

    public void SetPopularity(float popularity)
    {
        popularityBar.fillAmount = popularity / 100;
        popularityText.text = string.Format("{0:F0}%", popularity);
    }

    public void SetExperience(float experience)
    {
        experienceBar.fillAmount = experience / 100;
    }

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetTime(int time)
    {
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);

        string timeFormatted = string.Format("{0:D2}:{1:D2}", hours, minutes);

        timeText.text = timeFormatted;
    }
}
