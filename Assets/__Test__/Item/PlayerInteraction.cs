using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float checkRadius = 5f;

    public LayerMask interactableLayer;

    private Interactable closestInteractable;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius, interactableLayer);

        if (hits.Length == 0)
        {
            if (closestInteractable != null)
            {
                closestInteractable.ShowPrompt(false);
                closestInteractable = null;
            }
            return;
        }

        Interactable newClosest = null;
        float minDistance = float.MaxValue;

        foreach (Collider c in hits)
        {
            Interactable i = c.GetComponent<Interactable>();
            if (i != null && i.IsInRange(transform))
            {
                float dist = Vector3.Distance(transform.position, i.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    newClosest = i;
                }
            }
        }

        if (newClosest != closestInteractable)
        {
            if (closestInteractable != null)
                closestInteractable.ShowPrompt(false);

            closestInteractable = newClosest;

            if (closestInteractable != null)
                closestInteractable.ShowPrompt(true);
        }

        if (closestInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            closestInteractable.Interact();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
