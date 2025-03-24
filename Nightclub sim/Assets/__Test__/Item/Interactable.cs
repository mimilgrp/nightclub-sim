using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public float interactionRange = 3f;

    public Canvas promptCanvas;

    private void Start()
    {
        // Make the prompt hidden by default
        if (promptCanvas != null)
            promptCanvas.enabled = false;
    }

    public bool IsInRange(Transform playerTransform)
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        return (distance <= interactionRange);
    }

    public void ShowPrompt(bool show)
    {
        if (promptCanvas != null)
            promptCanvas.enabled = show;
    }

    public void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
    }
}
