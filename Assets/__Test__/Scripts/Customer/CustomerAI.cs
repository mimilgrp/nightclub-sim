using UnityEngine;
using System.Collections;

public class CustomerAI : MonoBehaviour
{
    public float stamina = 100f;
    public float money = 100f;

    [Range(0, 100)]
    public float danceChance = 25f;
    [Range(0, 100)]
    public float barChance = 25f;
    [Range(0, 100)]
    public float bathroomChance = 25f;
    [Range(0, 100)]
    public float wanderingChance = 25f;

    private bool isBusy = false;
    private CustomerMovementTest movement;

    private Transform dancefloorPosition;
    private Transform barPosition;
    private Transform bathroomPosition;
    private Transform wanderingPosition;

    private const float STAMINA_DECREASE_BAR = 5f;
    private const float STAMINA_DECREASE_DANCEFLOOR = 10f;
    private const float STAMINA_DECREASE_BATHROOM = 2f;
    private const float STAMINA_INCREASE_WANDERING = 5f;
    private const float MONEY_DECREASE_BAR = 5f;

    void Start()
    {
        movement = GetComponent<CustomerMovementTest>();

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
            Debug.Log("Customer destroyed");
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
        float totalChance = danceChance + barChance + bathroomChance + wanderingChance;
        float randomValue = Random.Range(0f, totalChance);
        float accumulatedChance = 0f;
        
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

    private IEnumerator MoveToAndHandleStamina(string position, Transform targetPosition, float staminaChange, float waitTime, float moneyChange = 0f)
    {
        if (targetPosition == null)
            yield break;

        Debug.Log($"Customer goes to " + position);
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
