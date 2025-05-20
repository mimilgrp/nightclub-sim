using System.Collections;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private int level = 0;
    private float experience = 0;

    void Start()
    {
        StartCoroutine(GainXPOverTime());
    }
    public void AddExperience(int p)
    {
        experience += p;

        if (experience > 100)
        {
            LevelUp();
        }
        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetExperience((int)experience);
        }
    }

    public void LevelUp()
    {
        experience = 0;
        level += 1;

        if (HUDDisplay.Instance != null)
        {
            HUDDisplay.Instance.SetLevel(level);
        }
    }

    private IEnumerator GainXPOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            AddExperience(10);
        }
    }
}
