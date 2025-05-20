using UnityEngine;
using System.Collections;
public class CustomerAI2 : MonoBehaviour
{
    private ZoneManager barZone, bathroomZone;
    public Animator animator;

    public int stamina = 100;
    public int money = 100;
    public int danceChance, barChance, bathroomChance, wanderingChance;
    public int satisfaction = 10;
    private bool isBusy = false;

    private CustomerMovementTest2 movement;
    private Transform  barPosition, bathroomPosition;
    public GameObject glassPrefab;
    public Transform glassSocket;
    private Transform leavingSpot;

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
        barPosition = GetTaggedPosition("Bar");
        bathroomPosition = GetTaggedPosition("Bathroom");
        leavingSpot = GetTaggedPosition("leavingspot");

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
        if (stamina <= 0)
        {
            CustomerAction action = ChooseAction();
            StartCoroutine(leaveClub());
            return;
        }

        if (!isBusy)
        {
            isBusy = true;
            StartCoroutine(ActionManager());
        }
    }

    private IEnumerator leaveClub()
    {
        animator.SetBool("IsWalking", true);
        yield return StartCoroutine(movement.MoveToExact(leavingSpot.position));
        animator.SetBool("IsWalking", false);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
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
        switch (action)
        {
            case CustomerAction.Bathroom:
                Transform bathroomSpot;
                if (bathroomZone != null && bathroomZone.TryEnter(out bathroomSpot))
                {
                    animator.SetBool("IsWalking", true);
                    yield return StartCoroutine(movement.MoveToExact(bathroomSpot.position));
                    animator.SetBool("IsWalking", false);

                    yield return StartCoroutine(RotateTowardsDirection(new Vector3(1f, 0f, -1f)));
                    Animator toiletAnimator = bathroomSpot.parent.GetComponentInChildren<Animator>();

                    toiletAnimator.SetTrigger("Toilet");
                    animator.SetTrigger("Toilet");

                    yield return new WaitForSeconds(5f);
                    bathroomZone.Exit(bathroomSpot);
                    stamina -= STAMINA_DECREASE_BATHROOM;
                    satisfaction += 2;
                }
                else if (bathroomZone != null && bathroomZone.CanQueue())
                {
                    bathroomZone.EnqueueCustomer(transform);
                    yield return StartCoroutine(WaitInQueue(bathroomZone, STAMINA_DECREASE_BATHROOM, 0));
                }
                else
                {
                    satisfaction -= 1;
                    int random = Random.Range(1, 3);
                    if (random == 1)
                    {
                        yield return StartCoroutine(PerformAction(CustomerAction.Wandering));
                    }
                    else
                    {
                        yield return StartCoroutine(PerformAction(CustomerAction.Dancefloor));
                    }
                }
                break;

            case CustomerAction.Bar:
                Transform barSpot;
                if (barZone != null && barZone.TryEnter(out barSpot))
                {
                    animator.SetBool("IsWalking", true);
                    yield return StartCoroutine(movement.MoveToExact(barSpot.position));
                    animator.SetBool("IsWalking", false);

                    yield return StartCoroutine(RotateTowardsDirection(new Vector3(1f, 0f, -1f)));
                    animator.SetTrigger("Drinking");
                    Transform drinksTransform = transform.Find("Drinks");
                    if (drinksTransform != null)
                    {
                        glassSocket = drinksTransform;
                    }
                    if (glassPrefab != null && glassSocket != null)
                    {
                        GameObject glass = Instantiate(glassPrefab, glassSocket);
                        glass.transform.localPosition = Vector3.zero;
                        glass.transform.localRotation = Quaternion.identity;
                        Destroy(glass, 4f);
                    }

                    yield return new WaitForSeconds(4f);

                    stamina -= STAMINA_DECREASE_BAR;
                    money -= MONEY_DECREASE_BAR;
                    barZone.Exit(barSpot);
                    satisfaction += 3;
                }
                else if (barZone.CanQueue())
                {
                    barZone.EnqueueCustomer(transform);
                    yield return StartCoroutine(WaitInQueue(barZone, MONEY_DECREASE_BAR, MONEY_DECREASE_BAR));
                }
                else
                {
                    satisfaction -= 1;
                    int random = Random.Range(1, 3);
                    if (random == 1)
                        yield return StartCoroutine(PerformAction(CustomerAction.Wandering));
                    else
                        yield return StartCoroutine(PerformAction(CustomerAction.Dancefloor));
                }
                break;

            case CustomerAction.Dancefloor:
                satisfaction += 1;
                yield return MoveToAndHandleStamina(CustomerAction.Dancefloor, danceZone, STAMINA_DECREASE_DANCEFLOOR, Random.Range(10f, 15f));
                break;

            case CustomerAction.Wandering:
                satisfaction += 1;
                yield return MoveToAndHandleStamina(CustomerAction.Wandering, wanderingZone, STAMINA_INCREASE_WANDERING, 8f);
                break;
        }
    }

    private IEnumerator MoveToAndHandleStamina(CustomerAction position, BoxCollider targetZone, int staminaChange, float waitTime, int moneyChange = 0)
    {
        if (targetZone == null)
            yield break;

        
        money -= moneyChange;
        if (position == CustomerAction.Dancefloor)
        {
            
            yield return StartCoroutine(movement.MoveToZoneRandom(targetZone));
            yield return StartCoroutine(RotateTowardsDirection(new Vector3(-1f, 0f, 1f)));
            animator.SetBool("IsDancing", true);
            yield return new WaitForSeconds(waitTime);
            animator.SetBool("IsDancing", false);
            stamina -= staminaChange;
        }
        else
        {
            stamina += staminaChange;
            animator.SetBool("IsWalking", true);
            yield return StartCoroutine(movement.WanderInZone(targetZone));
            animator.SetBool("IsWalking", false);
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

    IEnumerator RotateTowardsDirection(Vector3 direction)
    {
        float duration = 0.3f;
        float elapsed = 0f;

        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.LookRotation(direction.normalized);

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRot;
    }

}
