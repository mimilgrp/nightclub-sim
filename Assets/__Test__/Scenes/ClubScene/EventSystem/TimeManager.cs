using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TimeManager : MonoBehaviour
{
    public float gameTime = 77400f;
    public float gameTimeScale = 180f;

    private const int SecondsInDay = 86400;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        gameTime = (gameTime + Time.deltaTime * gameTimeScale) % SecondsInDay;

        if (gameTime < 0)
        {
            gameTime += SecondsInDay;
        }

        switch (DailyFlow.Instance.GameShift)
        {
            case DailyFlow.Shift.Preparation:
                if (IsTimeInRange(DailyFlow.Instance.showingTime, DailyFlow.Instance.closingTime))
                {
                    DailyFlow.Instance.Showing();
                }

                break;

            case DailyFlow.Shift.Showing:
                if (IsTimeInRange(DailyFlow.Instance.closingTime, DailyFlow.Instance.preparationTime))
                {
                    DailyFlow.Instance.Closing();
                }

                break;

            case DailyFlow.Shift.Closing:
                if (IsTimeInRange(DailyFlow.Instance.preparationTime, DailyFlow.Instance.showingTime))
                {
                    DailyFlow.Instance.Preparation();
                }

                break;
        }
    }

    public static TimeManager Instance { get; private set; }

    public static bool IsTimeInRange(float start, float end)
    {
        float gameTime = Instance.gameTime;

        return start < end
            ? gameTime >= start && gameTime < end
            : gameTime >= start || gameTime < end;
    }
}
