using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionPanel;

    private void Start()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void Resume()
    {
        if (MenuListener.Instance != null)
        {
            Time.timeScale = 1f;
            MenuListener.Instance.CloseMenu();
        }
        else
        {
            Debug.LogWarning("PauseMenu: MenuListener not found");
        }
    }

    public void OpenOptionPanel()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void ExitOptionPanel()
    {
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Debug.Log("PauseMenu: Quit game");
        Application.Quit();
    }
}
