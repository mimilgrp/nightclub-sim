using UnityEngine;

public class Popularity : MonoBehaviour
{
    public float popularity = 50f;

    public void IncreasePopularity(float value)
    {
        popularity = Mathf.Clamp(popularity + value, 0f, 100f);

    }
}