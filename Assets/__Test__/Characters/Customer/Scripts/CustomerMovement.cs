using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    public Transform targetDestination; 
    private NavMeshAgent customer;

    void Start()
    {
        customer = GetComponent<NavMeshAgent>();
        if (targetDestination != null)
        {
            customer.SetDestination(targetDestination.position);
        }
    }
}
