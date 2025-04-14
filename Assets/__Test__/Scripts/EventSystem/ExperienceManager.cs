using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private int level;
    private float experience;

    void Start()
    {
        level = 0;
        experience = 0;
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
}
