using UnityEngine;
using System.Collections.Generic;

public class ZoneManager : MonoBehaviour
{
    public List<Transform> spots = new List<Transform>(); // à remplir dans l’inspecteur
    private Dictionary<Transform, bool> spotStatus = new Dictionary<Transform, bool>();

    public int maxQueueSize = 3;
    private Queue<Transform> waitingQueue = new Queue<Transform>();

    void Start()
    {
        foreach (var spot in spots)
        {
            spotStatus[spot] = false;
        }
    }

    public bool HasFreeSpot() => GetFreeSpot() != null;

    public Transform GetFreeSpot()
    {
        foreach (var kvp in spotStatus)
        {
            if (!kvp.Value)
            {
                return kvp.Key;
            }
        }
        return null;
    }

    public bool TryEnter(out Transform assignedSpot)
    {
        assignedSpot = GetFreeSpot();
        if (assignedSpot != null)
        {
            spotStatus[assignedSpot] = true;
            return true;
        }
        return false;
    }

    public void Exit(Transform spot)
    {
        if (spotStatus.ContainsKey(spot))
        {
            spotStatus[spot] = false;
        }
    }

    public bool CanQueue() => waitingQueue.Count < maxQueueSize;

    public void EnqueueCustomer(Transform customer)
    {
        waitingQueue.Enqueue(customer);
    }

    public Transform DequeueCustomer()
    {
        return waitingQueue.Count > 0 ? waitingQueue.Dequeue() : null;
    }

    public bool IsQueueEmpty() => waitingQueue.Count == 0;
}
