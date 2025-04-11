using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (timeText == null)
        {
            Debug.LogError("Time Display Text is not assigned!");
        }
    }

    public static TimeDisplay Instance { get; private set; }

    public void SetTime(int time)
    {
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);

        string timeFormatted = string.Format("{0:D2}:{1:D2}", hours, minutes);

        timeText.text = timeFormatted;
    }
}
