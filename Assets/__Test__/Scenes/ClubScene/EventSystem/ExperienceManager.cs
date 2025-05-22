using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public float experience = 0;
    public float experienceStep = 100f;
    public float increaseStep = 20f;
    public int level = 0;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public static ExperienceManager Instance { get; private set; }

    public void AddExperience(float value)
    {
        experience += value;
        DayManager.Instance.experience += value;

        if (experience >= experienceStep)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= experienceStep;
        level += 1;
        experienceStep += increaseStep;
        increaseStep += 20;
        DayManager.Instance.level = level;
    }
}
