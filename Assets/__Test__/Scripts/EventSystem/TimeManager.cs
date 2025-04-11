using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class TimeManager : MonoBehaviour
{
    [Header("Game Time")]
    public float gameTime = 32400;

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
        dailyFlow = GetComponent<DailyFlow>();
        Preparation();
    }

    private void Update()
    {
        switch (currentShift)
        {
            case DailyFlow.Shift.Preparation:
                IncreaseGameTime(preparationTimeScale);
                if (gameTime >= showingTime || gameTime < closingTime)
                {
                    Showing();
                }
                break;
            case DailyFlow.Shift.Showing:
                IncreaseGameTime(showingTimeScale);
                if (gameTime >= closingTime && gameTime < preparationTime)
                {
                    Closing();
                }
                break;
            case DailyFlow.Shift.Closing:
                IncreaseGameTime(closingTimeScale);
                if (gameTime > closingTime && gameTime < showingTime)
                {
                    Preparation();
                }
                break;
        }

        TimeDisplay.Instance.SetTime((int)gameTime);
    }
    
    private void IncreaseGameTime(float timeScale)
    {
        gameTime += Time.deltaTime * timeScale;
        gameTime = ((gameTime % SecondsInDay) + SecondsInDay) % SecondsInDay;
    }

    private void Preparation()
    {
        dailyFlow.Preparation();
        currentShift = DailyFlow.Shift.Preparation;
        gameTime = preparationTime;
    }

    private void Showing()
    {
        dailyFlow.Showing();
        currentShift = DailyFlow.Shift.Showing;
        gameTime = showingTime;
    }

    private void Closing()
    {
        dailyFlow.Closing();
        currentShift = DailyFlow.Shift.Closing;
        gameTime = closingTime;
    }
}
