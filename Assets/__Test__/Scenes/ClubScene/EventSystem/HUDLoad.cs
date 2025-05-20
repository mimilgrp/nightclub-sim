using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDLoad : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }
}
