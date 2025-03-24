using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public GameObject pressEUI;

    public float detectionRadius = 5f;
    public LayerMask itemLayer;

    private InteractableItem closestItem;

    private void Start()
    {
            pressEUI.SetActive(false);
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, itemLayer);

        //find the closest InteractableItem
        InteractableItem newClosest = null;
        float minDist = float.MaxValue;

        foreach (Collider c in hits)
        {
            InteractableItem item = c.GetComponent<InteractableItem>();
            if (item != null)
            {
                float dist = Vector3.Distance(transform.position, item.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    newClosest = item;
                }
            }
        }

        // if the closest item changed then we update our reference
        if (newClosest != closestItem)
        {
            closestItem = newClosest;
        }

        // if there's no item then we hide the UI
        if (closestItem == null)
        {
            pressEUI.SetActive(false);
            return;
        }

        // show the UI if we are close enough
        pressEUI.SetActive(true);

        // if the player presses E call Interaction() from the script interactable object of this object
        if (Input.GetKeyDown(KeyCode.E))
        {
            closestItem.Interaction();
        }
    }

    // Visualize the detection sphere in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
