using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionPanel;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;

        Time.timeScale = 0f;
        mainPanel.SetActive(true);
        optionPanel.SetActive(false);

        Button resumeButton = transform.Find("MainPanel/ResumeButton")?.GetComponent<Button>();
        Button optionButton = transform.Find("MainPanel/OptionButton")?.GetComponent<Button>();
        Button quitButton = transform.Find("MainPanel/QuitButton")?.GetComponent<Button>();
        Button exitButton = transform.Find("OptionPanel/ExitButton")?.GetComponent<Button>();

        if (resumeButton != null)
            resumeButton.onClick.AddListener(Resume);

        if (optionButton != null)
            optionButton.onClick.AddListener(Option);

        if (quitButton != null)
            quitButton.onClick.AddListener(Quit);

        if (exitButton != null)
            exitButton.onClick.AddListener(backButton);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        if (MenuListener.Instance != null)
        {
            MenuListener.Instance.CloseMenu();
        }
        else
        {
            Debug.LogWarning("MenuListener non trouvé !");
        }
    }

    public void Option()
    {
        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quitter le jeu...");
        Application.Quit();
    }

    public void backButton()
    {
        optionPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
