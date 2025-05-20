using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    private GameObject player;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    public static DayManager Instance { get; private set; }

    public int DayNumber { get; private set; }
    public float DrinksPurchased { get; private set; }
    public float DrinksSold { get; private set; }
    public float MoneyEarned { get; private set; }
    public int CustomerVisits { get; private set; }
    public float PopularityEarned { get; private set; }
    public float ExperienceEarned { get; private set; }
    public int LevelEarned { get; private set; }

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            player = players[0];
            playerItems = player.GetComponent<PlayerItems>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        if (Instance == null)
        {
            Instance = this;
        }

        DayNumber = 0;
        ResetDayProperties();
    }

    public void AddDrinksPurchased(float amount)
    {
        DrinksPurchased -= amount;
        MoneyEarned -= amount;
    }

    public void AddCustomerVisit()
    {
        CustomerVisits++;
    }

    private void ResetDayProperties()
    {
        DayNumber++;
        DrinksPurchased = 0;
        DrinksSold = 0;
        MoneyEarned = 0;
        CustomerVisits = 0;
        PopularityEarned = 0;
        ExperienceEarned = 0;
        LevelEarned = 0;
    }

    public void EndDay()
    {
        playerItems.interactionFreeze = true;
        playerMovement.movementFreeze = true;
        Time.timeScale = 0f;

        SceneManager.LoadScene("EndDayPanel", LoadSceneMode.Additive);
    }

    public void ClosePanel()
    {
        playerItems.interactionFreeze = false;
        playerMovement.movementFreeze = false;
        Time.timeScale = 1f;

        if (SceneManager.GetSceneByName("EndDayPanel").isLoaded)
        {
            SceneManager.UnloadSceneAsync("EndDayPanel");
        }

        NewDay();
    }

    public void NewDay()
    {
        if (TimeManager.Instance != null)
        {
            ResetDayProperties();
            TimeManager.Instance.gameTime = TimeManager.Instance.preparationTime;
        }
    }
}
