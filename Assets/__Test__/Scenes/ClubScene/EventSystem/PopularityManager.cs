using UnityEngine;

public class PopularityManager : MonoBehaviour
{
    public float popularity = 50f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static PopularityManager Instance { get; private set; }

    public void AddPopularity(float value)
    {
        popularity = Mathf.Clamp(popularity + value, 0f, 100f);
        DayManager.Instance.popularity += popularity;
    }
}