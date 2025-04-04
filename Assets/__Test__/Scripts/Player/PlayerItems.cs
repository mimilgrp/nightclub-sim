using System;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public GameObject interactionUI;
    public float detectionRadius = 5f;
    public LayerMask interactionLayer;
    public Transform carryPoint;


    public bool interactionFreeze = false;
    private bool isCarrying;
    private MonoBehaviour nearestItem;
    
    private void Start()
    {
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (!interactionFreeze)
        {
            isCarrying = IsCarryingItem();
            MonoBehaviour newNearestItem = GetNearestItem();

            if (newNearestItem != nearestItem)
            {
                nearestItem = newNearestItem;
            }

            if (nearestItem == null)
            {
                interactionUI.SetActive(false);
            }
            else
            {
                interactionUI.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isCarrying)
                {
                    if (nearestItem is TakeDropItem takeDropItem)
                    {
                        // Take item
                        takeDropItem.Interact(carryPoint);
                    }
                    else if (nearestItem is InteractableItem interactableItem)
                    {
                        // Interact with an InteractableItem
                        Debug.Log("Can interact with : " + nearestItem.tag);
                        interactableItem.Interact(this.gameObject);
                    }

                }
                else if (isCarrying)
                {
                    if (nearestItem is StorageItem storageItem)
                    {
                        // Store item on shelf
                        storageItem.Interact(carryPoint.GetComponentInChildren<TakeDropItem>());
                        return;
                    }

                    // Drop item down if no shelf near
                    TakeDropItem carriedItem = carryPoint.GetComponentInChildren<TakeDropItem>();

                    if (carriedItem != null)
                    {
                        carriedItem.Interact(carryPoint);
                    }
                }
            }
        }
    }

    private MonoBehaviour GetNearestItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, interactionLayer);

        MonoBehaviour newNearestItem = null;

        float minDist = float.MaxValue;

        foreach (Collider c in hits)
        {
            if (!isCarrying)
            {
                if (c.TryGetComponent<TakeDropItem>(out var takeDropItem))
                {
                    if (takeDropItem.transform.IsChildOf(carryPoint))
                    {
                        continue;
                    }
                    else
                    {
                        float dist = Vector3.Distance(transform.position, takeDropItem.transform.position);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            newNearestItem = takeDropItem;
                        }
                    }
                }
                else if (c.TryGetComponent<InteractableItem>(out var interactableItem))
                {
                    float dist = Vector3.Distance(transform.position, interactableItem.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        newNearestItem = interactableItem;
                    }
                }
            }
            else
            {
                if (c.TryGetComponent<StorageItem>(out var storageItem))
                {
                    float dist = Vector3.Distance(transform.position, storageItem.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        newNearestItem = storageItem;
                    }
                }
            }
        }

        return newNearestItem;
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
