using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public float detectionRadius = 5f;
    public LayerMask interactionLayer;
    public Transform carryPoint;
    public InteractionUI interactionUI;

    public bool interactionFreeze = false;
    private bool isCarrying;

    private void Update()
    {
        if (!interactionFreeze)
        {
            isCarrying = IsCarryingItem();

            MonoBehaviour nearestItem = GetNearestItem();

            if (nearestItem == null)
            {
                interactionUI.HideInteraction();
            }
            else if (nearestItem is InteractableItem interactableItem)
            {
                interactionUI.ShowInteraction($"Interact {interactableItem.name}");
            }
            else if (nearestItem is StorageItem storageItem)
            {
                interactionUI.ShowInteraction($"FIll {storageItem.name}");
            }
            else if (nearestItem is BeverageItem beverageItem)
            {
                interactionUI.ShowInteraction($"Take {beverageItem.beverage} x{beverageItem.quantity}");
            }
            else if (nearestItem is TakeDropItem takeDropItem)
            {
                interactionUI.ShowInteraction($"Take {takeDropItem.name}");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isCarrying)
                {
                    if (nearestItem is StorageItem storageItem)
                    {
                        BeverageItem beverageItem = carryPoint.GetComponentInChildren<BeverageItem>();

                        storageItem.Fill(beverageItem);
                    }

                    TakeDropItem carriedItem = carryPoint.GetComponentInChildren<TakeDropItem>();

                    carriedItem.Drop();
                }
                else
                {
                    if (nearestItem is TakeDropItem takeDropItem)
                    {
                        takeDropItem.Take(carryPoint);
                    }
                    else if (nearestItem is InteractableItem interactableItem)
                    {
                        interactableItem.Interact();
                    }
                }
            }
        }
    }

    private MonoBehaviour GetNearestItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, interactionLayer);
        MonoBehaviour nearestItem = null;

        float minDist = float.MaxValue;

        foreach (Collider c in hits)
        {
            if (isCarrying)
            {
                if (c.TryGetComponent(out StorageItem storageItem))
                {
                    float dist = Vector3.Distance(transform.position, storageItem.transform.position);

                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestItem = storageItem;
                    }
                }
            }
            else
            {
                if (c.TryGetComponent(out TakeDropItem takeDropItem))
                {
                    if (!takeDropItem.transform.IsChildOf(carryPoint))
                    {
                        float dist = Vector3.Distance(transform.position, takeDropItem.transform.position);

                        if (dist < minDist)
                        {
                            minDist = dist;
                            nearestItem = takeDropItem;
                        }
                    }
                }
                else if (c.TryGetComponent(out InteractableItem interactableItem))
                {
                    float dist = Vector3.Distance(transform.position, interactableItem.transform.position);

                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestItem = interactableItem;
                    }
                }
            }
        }

        return nearestItem;
    }

    private bool IsCarryingItem()
    {
        return (carryPoint.childCount > 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
