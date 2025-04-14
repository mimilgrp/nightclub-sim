using UnityEngine;
using System.Collections;
public class CustomerAI2 : MonoBehaviour
{
    private ZoneManager barZone, bathroomZone;

    public int stamina = 100;
    public int money = 100;
    public int danceChance, barChance, bathroomChance, wanderingChance;
    public int satisfaction = 0;
    private bool isBusy = false;

    private CustomerMovementTest2 movement;
    private Transform /*dancefloorPosition,*/ barPosition, bathroomPosition/*, wanderingPosition*/;

    private BoxCollider danceZone;
    private BoxCollider wanderingZone;

    private CustomerAction? lastAction = null;

    private const int STAMINA_DECREASE_BAR = 5;
    private const int STAMINA_DECREASE_DANCEFLOOR = 10;
    private const int STAMINA_DECREASE_BATHROOM = 2;
    private const int STAMINA_INCREASE_WANDERING = 5;
    private const int MONEY_DECREASE_BAR = 5;
    public enum CustomerAction
    {
        Bathroom,
        Bar,
        Dancefloor,
        Wandering
    }

    void Start()
    {
        movement = GetComponent<CustomerMovementTest2>();
        createCustomer();
        //dancefloorPosition = GetTaggedPosition("Dancefloor");
        barPosition = GetTaggedPosition("Bar");
        bathroomPosition = GetTaggedPosition("Bathroom");
        //wanderingPosition = GetTaggedPosition("Wandering");

        danceZone = GetTaggedPosition("Dancefloor").GetComponentInChildren<BoxCollider>();
        wanderingZone = GetTaggedPosition("Wandering").GetComponentInChildren<BoxCollider>();

        barZone = barPosition.GetComponent<ZoneManager>();
        bathroomZone = bathroomPosition.GetComponent<ZoneManager>();
    }

    private void createCustomer()
    {
        int total = 100;
        int random = Random.Range(20, 32);
        danceChance = random;
        total -= random;

        random = Random.Range(20, 32);
        barChance = random;
        total -= random;

        random = Random.Range(20, 32);
        wanderingChance = random;
        total -= random;

        bathroomChance = total;
    }

    private Transform GetTaggedPosition(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        if (objectsWithTag.Length == 0)
            return null;

        int randomIndex = Random.Range(0, objectsWithTag.Length);

        return objectsWithTag[randomIndex].transform;
    }

    void Update()
    {
        if (stamina <= 0f)
        {
            Debug.Log("Customer satisfaction = " + satisfaction);
            Destroy(gameObject);
            return;
        }

        if (!isBusy)
        {
            isBusy = true;
            StartCoroutine(ActionManager());
        }
    }

    private CustomerAction ChooseAction()
    {
        CustomerAction chosen;
        int attempts = 0;

        do
        {
            float totalChance = danceChance + barChance + bathroomChance + wanderingChance;
            float randomValue = Random.Range(0f, totalChance);
            float accumulatedChance = 0f;

            if (randomValue <= (accumulatedChance += bathroomChance))
                chosen = CustomerAction.Bathroom;
            else if (randomValue <= (accumulatedChance += barChance))
                chosen = CustomerAction.Bar;
            else if (randomValue <= (accumulatedChance += danceChance))
                chosen = CustomerAction.Dancefloor;
            else
                chosen = CustomerAction.Wandering;

            attempts++;
            // max 10 tentatives, pour éviter une boucle infinie si toutes les actions sont égales
        } while (lastAction.HasValue && chosen == lastAction.Value && attempts < 10);

        lastAction = chosen;
        return chosen;
    }


    private IEnumerator ActionManager()
    {
        CustomerAction action = ChooseAction();
        yield return StartCoroutine(PerformAction(action));
        isBusy = false;
    }

    private IEnumerator PerformAction(CustomerAction action)
    {
        Debug.Log("Action en cours : "+ action);
        switch (action)
        {
            case CustomerAction.Bathroom:
                Transform bathroomSpot;
                if (bathroomZone != null && bathroomZone.TryEnter(out bathroomSpot))
                {
                    yield return StartCoroutine(movement.MoveToExact(bathroomSpot.position));
                    stamina += STAMINA_DECREASE_BATHROOM;
                    yield return new WaitForSeconds(Random.Range(5f, 10f));
                    bathroomZone.Exit(bathroomSpot);
                }
                else if (bathroomZone != null && bathroomZone.CanQueue())
                {
                    bathroomZone.EnqueueCustomer(transform);
                    yield return StartCoroutine(WaitInQueue(bathroomZone, STAMINA_DECREASE_BATHROOM, 0));
                }
                else
                {
                    int random = Random.Range(1, 3);
                    if(random == 1)
                        yield return StartCoroutine(PerformAction(CustomerAction.Wandering));
                    else
                        yield return StartCoroutine(PerformAction(CustomerAction.Dancefloor));
                }
                break;

            case CustomerAction.Bar:
                Transform barSpot;
                if (barZone != null && barZone.TryEnter(out barSpot))
                {
                    yield return StartCoroutine(movement.MoveToExact(barSpot.position));
                    stamina += STAMINA_DECREASE_BAR;
                    money -= MONEY_DECREASE_BAR;
                    yield return new WaitForSeconds(Random.Range(8f, 15f));
                    barZone.Exit(barSpot);
                }
                else if (barZone.CanQueue())
                {
                    barZone.EnqueueCustomer(transform);
                    yield return StartCoroutine(WaitInQueue(barZone, MONEY_DECREASE_BAR, MONEY_DECREASE_BAR));
                }
                else
                {
                    int random = Random.Range(1, 3);
                    if (random == 1)
                        yield return StartCoroutine(PerformAction(CustomerAction.Wandering));
                    else
                        yield return StartCoroutine(PerformAction(CustomerAction.Dancefloor));
                }
                break;

            case CustomerAction.Dancefloor:
                yield return MoveToAndHandleStamina(CustomerAction.Dancefloor, danceZone, STAMINA_DECREASE_DANCEFLOOR, Random.Range(10f, 15f));
                break;

            case CustomerAction.Wandering:
                yield return MoveToAndHandleStamina(CustomerAction.Wandering, wanderingZone, STAMINA_INCREASE_WANDERING, 8f);
                break;
        }
    }

    private IEnumerator MoveToAndHandleStamina(CustomerAction position, BoxCollider targetZone, int staminaChange, float waitTime, int moneyChange = 0)
    {
        if (targetZone == null)
            yield break;

        stamina += staminaChange;
        money -= moneyChange;
        if (position == CustomerAction.Dancefloor)
        {
            yield return StartCoroutine(movement.MoveToZoneRandom(targetZone));
            yield return new WaitForSeconds(waitTime);
        }
        else
        {
            yield return StartCoroutine(movement.WanderInZone(targetZone));
            yield break;
        }
    }

    private IEnumerator WaitInQueue(ZoneManager zone, int staminaChange, int moneyChange)
    {
        float waitTime = 0f;
        float maxWaitTime = 15f;

        while (waitTime < maxWaitTime)
        {
            if (zone.HasFreeSpot())
            {
                Transform spot;
                if (zone.TryEnter(out spot))
                {
                    yield return StartCoroutine(movement.MoveToExact(spot.position));
                    stamina += staminaChange;
                    money -= moneyChange;
                    yield return new WaitForSeconds(Random.Range(3f, 8f)); // ou paramétrable aussi si tu veux
                    zone.Exit(spot);
                    yield break;
                }
            }

            waitTime += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(PerformAction(CustomerAction.Wandering)); // trop d'attente
    }
}
