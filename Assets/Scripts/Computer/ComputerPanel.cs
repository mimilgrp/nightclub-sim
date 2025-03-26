using UnityEngine;
using UnityEngine.SceneManagement;


public class ComputerPanel : MonoBehaviour
{
    public void OpenComputer()
    {
        SceneManager.LoadScene("ComputerPanel", LoadSceneMode.Additive);
    }

    public void ExitComputer()
    {
        SceneManager.UnloadSceneAsync("ComputerPanel");
    }
}