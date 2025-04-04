using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CustomerMovementTest : MonoBehaviour
{
    private NavMeshAgent agent;
    public int zoneRadius = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator MoveTo(Vector3 zoneCenter)
    {
        Vector3 destination = GetRandomPointInZone(zoneCenter);
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

    private Vector3 GetRandomPointInZone(Vector3 zoneCenter)
    {
        int randomPosX = Random.Range(-zoneRadius, zoneRadius);
        int randomPosZ = Random.Range(-zoneRadius, zoneRadius);
        Vector3 destination = new Vector3(zoneCenter.x + randomPosX, 1, zoneCenter.z + randomPosZ);

        return destination;
    }
}
