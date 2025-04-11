using UnityEngine;
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
                gameTime += Time.deltaTime * preparationTimeScale;
                if (gameTime >= showingTime && gameTime >= showingTime)
                {
                    Showing();
                }
                break;
            case DailyFlow.Shift.Showing:
                gameTime += Time.deltaTime * showingTimeScale; 
                if (gameTime >= closingTime)
                {
                    Closing();
                }
                break;
            case DailyFlow.Shift.Closing:
                gameTime += Time.deltaTime * closingTimeScale; 
                if (gameTime >= closingTime + 1)
                {
                    Preparation();
                }
                break;
        }

        TimeDisplay.Instance.SetTime((int)gameTime);
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
    }

    private void Closing()
    {
        dailyFlow.Closing();
        currentShift = DailyFlow.Shift.Closing;
    }
}
