using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("ClubScene", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Debug.Log("MainMenu: Quit game");
        Application.Quit();
    }
}
