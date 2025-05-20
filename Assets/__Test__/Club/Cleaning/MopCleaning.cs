using UnityEngine;

public class MopCleaning : MonoBehaviour
{
    public float detectionRadius = 1f;
    public LayerMask interactionLayer;
    public string targetTag;

    private void Update()
    {
        GameObject nearestItem = GetNearestItem();
        
        if (nearestItem != null)
        {
            Debug.Log("Mop: Stain cleaned");
            Destroy(nearestItem);
        }
    }

    private GameObject GetNearestItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, interactionLayer);

        GameObject newNearestItem = null;
        float minDist = float.MaxValue;

        foreach (Collider c in hits)
        {           
            if (c.CompareTag(targetTag))
            {
                float dist = Vector3.Distance(transform.position, c.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    newNearestItem = c.gameObject;
                }
            }
        }

        return newNearestItem;
    }
}
