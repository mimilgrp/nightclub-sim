using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light gameLight;

    [Header("Tags")]
    public string preparingLightsTag = "PreparingLight";
    public string showingLightsTag = "ShowingLight";

    [Header("Delay")]
    public float delayBeforeShowingTime = 3600;

    private GameObject[] preparingLights;
    private GameObject[] showingLights;

    public enum Light
    {
        Showing,
        Preparing
    }

    private void Start()
    {
        preparingLights = GameObject.FindGameObjectsWithTag(preparingLightsTag);
        showingLights = GameObject.FindGameObjectsWithTag(showingLightsTag);
    }

    private void Update()
    {
        float gameTime = TimeManager.Instance.gameTime;
        float showingTime = DailyFlow.Instance.showingTime;
        float closingTime = DailyFlow.Instance.closingTime;

        if (TimeManager.IsTimeInRange((showingTime - delayBeforeShowingTime), closingTime))
        {
            if (gameLight != Light.Showing)
            {
                SwitchToShowingLights();
                gameLight = Light.Showing;
            }
            SwitchToShowingLights();
        }
        else
        {
            if (gameLight != Light.Preparing)
            {
                SwitchToPreparingLights();
                gameLight = Light.Preparing;
            }
            SwitchToPreparingLights();
        }
    }

    public void SwitchToPreparingLights()
    {
        SetPreparingLights(true);
        SetShowingLights(false);
    }

    public void SwitchToShowingLights()
    {
        SetPreparingLights(false);
        SetShowingLights(true);
    }

    private void SetPreparingLights(bool value)
    {
        foreach (GameObject preparingLight in preparingLights)
        {
            preparingLight.SetActive(value);
        }
    }

    private void SetShowingLights(bool value)
    {
        foreach (GameObject showingLight in showingLights)
        {
            showingLight.SetActive(value);
        }
    }
}
