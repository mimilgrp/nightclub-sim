using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    public void Interact(GameObject Player)
    {

        PlayerItems playerItems = Player.GetComponent<PlayerItems>();
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        
        Debug.Log("Interact item: " + gameObject.name);

        if (CompareTag("Computer"))
        {
            playerItems.interactionFreeze = true;
            playerMovement.movementFreeze = true;
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (scene.name == "ComputerPanel")
                {
                    SetupExitButton(playerItems, playerMovement);
                    // Unsubscribe after
                    SceneManager.sceneLoaded -= (scene, mode) => { };
                }
            };
            SceneManager.LoadScene("ComputerPanel", LoadSceneMode.Additive);
            SetupExitButton(playerItems, playerMovement);
        }
    }

    private void SetupExitButton(PlayerItems playerItems, PlayerMovement playerMovement)
    {

        GameObject exitButtonObj = GameObject.Find("ExitButton");
        if (exitButtonObj != null)
        {
            Button exitButton = exitButtonObj.GetComponent<Button>();
            if (exitButton != null)
            {
                exitButton.onClick.RemoveAllListeners();
                exitButton.onClick.AddListener(() =>
                {
                    SceneManager.UnloadSceneAsync("ComputerPanel");
                    playerItems.interactionFreeze = false;
                    playerMovement.movementFreeze = false;
                });
            }
        }
    }
}
