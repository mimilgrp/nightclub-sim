using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private Image xpBar;
    [SerializeField] private TextMeshProUGUI currentLevel;
    void Start()
    {
        if (Instance == null) Instance = this;
    }

    public static ExperienceBar Instance { get; private set; }

    public void DisplayXpBar(int xp)
    {
        xpBar.fillAmount = xp/100;
    }
    public void DisplayCurrentLevel(int level)
    {
        currentLevel.text = level.ToString();
    }

}

