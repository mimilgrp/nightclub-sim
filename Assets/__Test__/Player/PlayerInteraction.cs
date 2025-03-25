using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject pressEUI;

    public float detectionRadius = 5f;

    public LayerMask interactionLayers;

    public Transform carryPoint;

    private MonoBehaviour closestInteractable;

    private void Start()
    {
        pressEUI.SetActive(false);
    }

    void Update()
    {
        bool isCarrying = IsCarryingItem();

        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, interactionLayers);

        MonoBehaviour newClosest = null;
        float minDist = float.MaxValue;

        foreach (Collider c in hits)
        {
            if (!isCarrying)
            {
                InteractableItem item = c.GetComponent<InteractableItem>();
                if (item != null)
                {
                    if (item.transform.IsChildOf(carryPoint))
                    {
                        continue;
                    }
                    else
                    {
                        float dist = Vector3.Distance(transform.position, item.transform.position);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            newClosest = item;
                        }
                    }
                }
            }
            else
            {
                
                ShelfStockage shelf = c.GetComponent<ShelfStockage>();
                if (shelf != null)
                {
                    float dist = Vector3.Distance(transform.position, shelf.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        newClosest = shelf;
                    }
                }
            }
        }

        if (newClosest != closestInteractable)
        {
            closestInteractable = newClosest;
        }

        if (closestInteractable == null)
        {
            pressEUI.SetActive(false);
            return;
        }
        else
        {
            pressEUI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarrying && closestInteractable is InteractableItem)
            {
                // Pick up 
                InteractableItem item = (InteractableItem)closestInteractable;
                item.Interaction();
            }
            else if (isCarrying && closestInteractable is ShelfStockage)
            {
                // Store item on shelf
                Debug.Log("HERE");
                ShelfStockage shelf = (ShelfStockage)closestInteractable;
                shelf.Interact(this.gameObject);
            }
        }
    }

    private bool IsCarryingItem()
    {
        return (carryPoint != null && carryPoint.childCount > 0);
    }

    // For debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
