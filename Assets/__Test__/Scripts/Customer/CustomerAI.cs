using UnityEngine;
using System.Collections;

public class CustomerAI : MonoBehaviour
{
    public int stamina = 100;
    public int money = 100;
    public int danceChance, barChance, bathroomChance, wanderingChance;
    public int satisfaction = 0;
    private bool isBusy = false;

    private CustomerMovementTest movement;
    private Transform dancefloorPosition, barPosition, bathroomPosition, wanderingPosition;

    private const int STAMINA_DECREASE_BAR = 5;
    private const int STAMINA_DECREASE_DANCEFLOOR = 10;
    private const int STAMINA_DECREASE_BATHROOM = 2;
    private const int STAMINA_INCREASE_WANDERING = 5;
    private const int MONEY_DECREASE_BAR = 5;

    void Start()
    {
        movement = GetComponent<CustomerMovementTest>();

        danceChance = Random.Range(30, 50);
        barChance = Random.Range(15, 30);
        bathroomChance = Random.Range(10, 25);
        wanderingChance = Random.Range(10, 20);

        dancefloorPosition = GetTaggedPosition("Dancefloor");
        barPosition = GetTaggedPosition("Bar");
        bathroomPosition = GetTaggedPosition("Bathroom");
        wanderingPosition = GetTaggedPosition("Wandering");
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
        int totalChance = danceChance + barChance + bathroomChance + wanderingChance;
        int randomValue = Random.Range(0, totalChance);
        int accumulatedChance = 0;
        
        if (randomValue <= (accumulatedChance += bathroomChance))
            return CustomerAction.Bathroom;
        if (randomValue <= (accumulatedChance += barChance))
            return CustomerAction.Bar;
        if (randomValue <= (accumulatedChance += danceChance))
            return CustomerAction.Dancefloor;

        return CustomerAction.Wandering;
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
                yield return MoveToAndHandleStamina("Bathroom", bathroomPosition, STAMINA_DECREASE_BATHROOM, Random.Range(5f, 10f));
                break;

            case CustomerAction.Bar:
                yield return MoveToAndHandleStamina("Bar", barPosition, STAMINA_DECREASE_BAR, Random.Range(3f, 8f), MONEY_DECREASE_BAR);
                break;

            case CustomerAction.Dancefloor:
                yield return MoveToAndHandleStamina("Dancefloor", dancefloorPosition, STAMINA_DECREASE_DANCEFLOOR, Random.Range(10f, 25f));
                break;

            case CustomerAction.Wandering:
                yield return MoveToAndHandleStamina("Wandering", wanderingPosition, STAMINA_INCREASE_WANDERING, 5f);
                break;
        }
    }

    private IEnumerator MoveToAndHandleStamina(string position, Transform targetPosition, int staminaChange, float waitTime, int moneyChange = 0)
    {
        if (targetPosition == null)
            yield break;

        yield return StartCoroutine(movement.MoveTo(targetPosition.position));
        stamina += staminaChange;
        money -= moneyChange;

        yield return new WaitForSeconds(waitTime);
    }

    public enum CustomerAction
    {
        Bathroom,
        Bar,
        Dancefloor,
        Wandering
    }
}
