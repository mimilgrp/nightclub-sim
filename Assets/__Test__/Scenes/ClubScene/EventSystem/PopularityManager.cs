using UnityEngine;

public class Popularity : MonoBehaviour
{
    public float popularity = 50f;

    private void Update()
    {
        DisplayPopularity();
    }

    public void IncreasePopularity(float value)
    {
        popularity = Mathf.Clamp(popularity + value, 0f, 100f);

    }

    public void DisplayPopularity()
    {
        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetPopularity(popularity);
        }
    }
}