using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    private GameObject player;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    public int day;
    public float drinksPurchased;
    public float drinksSold;
    public float money;
    public int customers;
    public float popularity;
    public float experience;
    public int level;

    public enum Transaction
    {
        DrinksPurchased,
        DrinksSold
    }

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

        day = 0;
        ResetDayProperties();
    }

    public static DayManager Instance { get; private set; }

    public void AddMoney(float value, Transaction transaction)
    {
        switch (transaction)
        {
            case Transaction.DrinksPurchased:
                drinksPurchased += value;
                break;
            case Transaction.DrinksSold:
                drinksSold += value;
                break;
        }

        money += value;
    }

    private void ResetDayProperties()
    {
        day++;
        drinksPurchased = 0;
        drinksSold = 0;
        money = 0;
        customers = 0;
        popularity = 0;
        experience = 0;
        level = 0;
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
            DailyFlow.Instance.Preparation();
        }
    }
}
