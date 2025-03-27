using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableItem : MonoBehaviour
{
    public void Interact()
    {
        
        Debug.Log("Interact item: " + gameObject.name);

        if (CompareTag("Computer"))
        {
            SceneManager.LoadScene("ComputerPanel", LoadSceneMode.Additive);
        }
    }
}
