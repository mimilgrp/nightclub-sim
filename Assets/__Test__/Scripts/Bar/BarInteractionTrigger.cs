using UnityEngine;

public class BarInteractionTrigger : MonoBehaviour
{
    private bool playerInTrigger = false;
    private GameObject playerRef;
    public GameObject interactionUI;
    private void Start()
    {
        interactionUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            playerRef = other.gameObject;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            playerRef = null;
            interactionUI.SetActive(false);
        }
    }

    public bool CanInteract()
    {
        return playerInTrigger;
    }

    public GameObject GetPlayer() => playerRef;
}
