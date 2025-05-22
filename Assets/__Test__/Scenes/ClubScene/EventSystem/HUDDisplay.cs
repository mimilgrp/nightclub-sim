using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
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

    [Header("Notifications")]
    public Transform notificationParent;
    public GameObject notificationPrefab;
    public float notificationDuration = 5f;

    [Header("Welcome Notification")]
    public GameObject welcomeNotificationPrefab;
    public float welcomeNotificationDuration = 10f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        ShowWelcomeNotification();
    }
    
    private void Update()
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

    public static HUDDisplay Instance { get; private set; }

    public void ShowNotification(string message)
    {
        GameObject notification = Instantiate(notificationPrefab, notificationParent);
        TMP_Text text = notification.GetComponentInChildren<TMP_Text>();
        text.text = message.ToUpper();

        CanvasGroup canvasGroup = notification.GetComponent<CanvasGroup>();
        StartCoroutine(FadeNotification(notification, canvasGroup, notificationDuration));
    }

    private void ShowWelcomeNotification()
    {
        GameObject notification = Instantiate(welcomeNotificationPrefab, notificationParent);

        CanvasGroup canvasGroup = notification.GetComponent<CanvasGroup>();
        StartCoroutine(FadeNotification(notification, canvasGroup, welcomeNotificationDuration));
    }

    IEnumerator FadeNotification(GameObject notification, CanvasGroup canvasGroup, float duration)
    {
        for (float t = 0; t < 1; t += Time.deltaTime * 2)
        {
            canvasGroup.alpha = t;
            yield return null;
        }
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(duration);

        for (float t = 1; t > 0; t -= Time.deltaTime * 2)
        {
            canvasGroup.alpha = t;
            yield return null;
        }

        Destroy(notification);
    }
}
