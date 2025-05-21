using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractableItem : MonoBehaviour
{
    PlayerItems playerItems;
    PlayerMovement playerMovement;
    InteractionUI interactionUI;

    public enum InteractionItem
    {
        Computer,
        Bar
    }

    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        playerItems = Player.GetComponent<PlayerItems>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        interactionUI = Player.GetComponentInChildren<InteractionUI>();
    }
    public void Interact()
    {

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
            StartCoroutine(HandleInteraction(0.5f, InteractionItem.Computer));
        }

        else if (CompareTag("BarInteraction"))
        {
            StartCoroutine(HandleInteraction(2f, InteractionItem.Bar));
        }
    }

    private IEnumerator HandleInteraction(float time, InteractionItem InteractionItem)
    {
        playerItems.interactionFreeze = true;
        playerMovement.movementFreeze = true;
        switch (InteractionItem)
        {
            case InteractionItem.Computer:

                interactionUI.showAction(time);
                yield return new WaitForSeconds(time);

                SceneManager.LoadScene("ComputerPanel", LoadSceneMode.Additive);
                break;

            case InteractionItem.Bar:

                GameObject barObject = GameObject.Find("Bar");
                BarManager barManager = barObject.GetComponent<BarManager>();
                interactionUI.showAction(time);
                yield return new WaitForSeconds(time);
                playerItems.interactionFreeze = false;
                playerMovement.movementFreeze = false;
                barManager.ServeNextCustomer();
                break;
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