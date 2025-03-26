using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class CustomerMovementTest : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);

        while (agent.pathPending)
        {
            yield return null;
        }


        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }


    }
}
