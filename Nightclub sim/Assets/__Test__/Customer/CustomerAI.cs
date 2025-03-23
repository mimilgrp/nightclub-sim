using UnityEngine;
using System.Collections;

public class CustomerAI : MonoBehaviour
{
    public float stamina = 100f;
    public float argent = 100f;

    // Table of percentage of chances to do an action
    // [ChanceDanse, ChanceDrink, ChanceToilet, ChanceWandering]
    public float[] characteristic = new float[] { 25f, 25f, 25f, 25f };

    private bool isBusy = false;
    private CustomerMovementTest movement;

    private Transform dancefloorPosition;
    private Transform barPosition;
    private Transform bathroomPosition;
    private Transform wanderingPosition;

    void Start()
    {
        movement = GetComponent<CustomerMovementTest>();

        if (GameObject.FindGameObjectWithTag("Dancefloor") != null)
            dancefloorPosition = GameObject.FindGameObjectWithTag("Dancefloor").transform;

        if (GameObject.FindGameObjectWithTag("Bar") != null)
            barPosition = GameObject.FindGameObjectWithTag("Bar").transform;

        if (GameObject.FindGameObjectWithTag("Bathroom") != null)
            bathroomPosition = GameObject.FindGameObjectWithTag("Bathroom").transform;

        if (GameObject.FindGameObjectWithTag("Wandering") != null)
            wanderingPosition = GameObject.FindGameObjectWithTag("Wandering").transform;
    }

    private void Update()
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

        float randomValue = Random.Range(0f, 100f);
        // [ChanceDanse, ChanceDrink, ChanceToilet, ChanceWandering]

        float danceChance = characteristic[0];
        float barChance = characteristic[1];
        float bathChance = characteristic[2];
        float wandChance = characteristic[3];

        if (randomValue <= danceChance)
        {
            return "Dancefloor";
        }
        else if (randomValue <= (danceChance + barChance))
        {
            return "Bar";
        }
        else if (randomValue <= (danceChance + barChance + bathChance))
        {
            return "Bathroom";
        }
        else
        {
            return "Wandering";
        }

    }

    private IEnumerator ActionManager()
    {

        string action = ChooseAction();

        switch (action)
        {
            case "Bathroom":
                Debug.Log("Customer goes to the toilet");
                yield return StartCoroutine(movement.MoveTo(bathroomPosition.position));
                stamina -= 2f;
                yield return new WaitForSeconds(Random.Range(5f, 10f));
                break;

            case "Bar":
                Debug.Log("Customer goes to the bar");
                yield return StartCoroutine(movement.MoveTo(barPosition.position));
                argent -= 5f;
                stamina -= 5f;
                yield return new WaitForSeconds(Random.Range(3f, 8f));
                break;

            case "Dancefloor":
                Debug.Log("Customer goes to the dancefloor");
                yield return StartCoroutine(movement.MoveTo(dancefloorPosition.position));
                stamina -= 10f;
                yield return new WaitForSeconds(Random.Range(10f, 25f));
                break;

            case "Wandering":
                Debug.Log("Customer is wandering");
                yield return StartCoroutine(movement.MoveTo(wanderingPosition.position));
                stamina += 5f;
                yield return new WaitForSeconds(5f);
                break;
        }

        isBusy = false;
    }
}
