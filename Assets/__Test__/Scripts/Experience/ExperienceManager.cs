using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperienceManager : MonoBehaviour
{
    private int currentLevel;
    private int progression;

    void Start()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        currentLevel = 0;
        progression = 0;
    }
    public void addProgression(int p)
    {
        if (progression + p > 100)
        {
            levelup();
        }
        else
        {
            progression += p;
            updateExperienceBar();
        }
    }

    public void levelup()
    {
        progression = 0;
        currentLevel += 1;
        updateExperienceBar();
        updateLevel();
    }

    public void updateExperienceBar()
    {
        if (ExperienceBar.Instance != null)
        {
            ExperienceBar.Instance.DisplayXpBar(progression);
        }
        else
        {
            Debug.LogWarning("HUD pas encore prêt");
        }
    }
    public void updateLevel()
    {
        if (ExperienceBar.Instance != null)
        {
            ExperienceBar.Instance.DisplayCurrentLevel(currentLevel);
        }
        else
        {
            Debug.LogWarning("HUD pas encore prêt");
        }
    }
}
