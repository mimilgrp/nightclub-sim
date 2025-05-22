using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CustomerMovementTest2 : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;
    public float wanderingWaitTimeMin = 1f;
    public float wanderingWaitTimeMax = 3f;
    public int wanderingSteps = 5;
    public ParticleSystem footSmoke;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Go to (ex : un BarSit/Toilet Sit)
    public IEnumerator MoveToExact(Vector3 position)
    {
        agent.SetDestination(position);
        animator.SetBool("IsWalking", true);
        if (footSmoke != null)
        {
            footSmoke.Play();
        }

        while (agent.pathPending)
        {
            yield return null;
        }

        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        if (footSmoke != null)
        {
            footSmoke.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        animator.SetBool("IsWalking", false);
    }
    public IEnumerator MoveToZoneRandom(BoxCollider zone)
    {
        Vector3 destination = GetRandomPointInBox(zone);
        agent.SetDestination(destination);
        animator.SetBool("IsWalking", true);
        if (footSmoke != null)
        {
            footSmoke.Play();
        }

        while (agent.pathPending)
        {
            yield return null;
        }

        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        if (footSmoke != null)
        {
            footSmoke.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        animator.SetBool("IsWalking", false);
    }

    // Wandering (walking multiple times)
    public IEnumerator WanderInZone(BoxCollider zone)
    {
        for (int i = 0; i < wanderingSteps; i++)
        {
            Vector3 destination = GetRandomPointInBox(zone);
            agent.SetDestination(destination);
            animator.SetBool("IsWalking", true);
            if (footSmoke != null)
            {
                footSmoke.Play();
            }

            while (agent.pathPending)
            {
                yield return null;
            }

            while (agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            if (footSmoke != null)
            {
                footSmoke.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }

            animator.SetBool("IsWalking", false);
            animator.SetBool("Wandering", true);
            yield return new WaitForSeconds(Random.Range(wanderingWaitTimeMin, wanderingWaitTimeMax));
            animator.SetBool("Wandering", false);
        }
    }

    private Vector3 GetRandomPointInBox(BoxCollider box)
    {
        Vector3 localCenter = box.center;
        Vector3 localSize = box.size;

        float localX = Random.Range(-localSize.x / 2f, localSize.x / 2f);
        float localZ = Random.Range(-localSize.z / 2f, localSize.z / 2f);
        float localY = 1f;

        Vector3 localPoint = new Vector3(localX, localY, localZ) + localCenter;
        return box.transform.TransformPoint(localPoint); // convertit en world position
    }

}
