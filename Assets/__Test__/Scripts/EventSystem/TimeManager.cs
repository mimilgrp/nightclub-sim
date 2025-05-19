using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Game Time")]
    public float gameTime = 32400f;

    [Header("Time Scales")]
    public float preparationTimeScale = 180f;
    public float showingTimeScale = 120f;
    public float closingTimeScale = 0f;

    [Header("Time Shifts")]
    public int preparationTime = 57600;
    public int showingTime = 82800;
    public int closingTime = 14400;

    private DailyFlow dailyFlow;
    private DailyFlow.Shift currentShift;

    private const int SecondsInDay = 86400;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        dailyFlow = GetComponent<DailyFlow>();
        SetShift(DailyFlow.Shift.Preparation, (int)gameTime);
    }

    private void Update()
    {
        switch (currentShift)
        {
            case DailyFlow.Shift.Preparation:
                UpdateGameTime(preparationTimeScale);
                if (IsTimeInRange(showingTime, closingTime))
                    SetShift(DailyFlow.Shift.Showing, showingTime);
                break;

            case DailyFlow.Shift.Showing:
                UpdateGameTime(showingTimeScale);
                if (IsTimeInRange(closingTime, preparationTime))
                    SetShift(DailyFlow.Shift.Closing, closingTime);
                break;

            case DailyFlow.Shift.Closing:
                UpdateGameTime(closingTimeScale);
                if (IsTimeInRange(preparationTime, showingTime))
                    SetShift(DailyFlow.Shift.Preparation, preparationTime);
                break;
        }
        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetTime((int)gameTime);
        }
    }

    public static TimeManager Instance { get; private set; }

    private void UpdateGameTime(float timeScale)
    {
        gameTime = (gameTime + Time.deltaTime * timeScale) % SecondsInDay;
        if (gameTime < 0) gameTime += SecondsInDay;
    }

    private bool IsTimeInRange(int start, int end)
    {
        return start < end
            ? gameTime >= start && gameTime < end
            : gameTime >= start || gameTime < end;
    }

    private void SetShift(DailyFlow.Shift shift, int time)
    {
        currentShift = shift;
        gameTime = time;

        switch (shift)
        {
            case DailyFlow.Shift.Preparation: dailyFlow.Preparation(); break;
            case DailyFlow.Shift.Showing: dailyFlow.Showing(); break;
            case DailyFlow.Shift.Closing: dailyFlow.Closing(); break;
        }
    }
}
