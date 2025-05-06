using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuListener : MonoBehaviour
{
    private GameObject player;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private bool isMenuOpen = false;

    public static MenuListener Instance { get; private set; }

    private void Start()
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    private void OpenMenu()
    {
        isMenuOpen = true;
        playerItems.interactionFreeze = true;
        playerMovement.movementFreeze = true;
        Time.timeScale = 0f;

        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }

    public void CloseMenu()
    {
        isMenuOpen = false;
        playerItems.interactionFreeze = false;
        playerMovement.movementFreeze = false;
        Time.timeScale = 1f;

        if (SceneManager.GetSceneByName("PauseMenu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
        }
    }
}
