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
    public float toiletChance = 25f;
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

        dancefloorPosition = GameObject.FindGameObjectWithTag("Dancefloor").transform;
        barPosition = GameObject.FindGameObjectWithTag("Bar").transform;
        bathroomPosition = GameObject.FindGameObjectWithTag("Bathroom").transform;
        wanderingPosition = GameObject.FindGameObjectWithTag("Wandering").transform;
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

    private string ChooseAction()
    {
        float totalChance = danceChance + barChance + toiletChance + wanderingChance;
        float randomValue = Random.Range(0f, totalChance);
        float accumulatedChance = 0f;
        accumulatedChance = danceChance;
        if (randomValue <= accumulatedChance)
            return "Dancefloor";
        if (randomValue <= (accumulatedChance += barChance))
            return "Bar";
        if (randomValue <= (accumulatedChance += toiletChance))
            return "Bathroom";
        
        return "Wandering";
    }

    private IEnumerator ActionManager()
    {
        string action = ChooseAction();
        yield return StartCoroutine(PerformAction(action));
        isBusy = false;
    }

    private IEnumerator PerformAction(string action)
    {
        switch (action)
        {
            case "Bathroom":
                yield return MoveToAndHandleStamina(action, bathroomPosition, STAMINA_DECREASE_BATHROOM, Random.Range(5f, 10f));
                break;

            case "Bar":
                yield return MoveToAndHandleStamina(action, barPosition, STAMINA_DECREASE_BAR, Random.Range(3f, 8f), MONEY_DECREASE_BAR);
                break;

            case "Dancefloor":
                yield return MoveToAndHandleStamina(action, dancefloorPosition, STAMINA_DECREASE_DANCEFLOOR, Random.Range(10f, 25f));
                break;

            case "Wandering":
                yield return MoveToAndHandleStamina(action, wanderingPosition, STAMINA_INCREASE_WANDERING, 5f);
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
}
