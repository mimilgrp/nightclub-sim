using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{

    public GameObject interactionUI_interaction;
    public GameObject interactionUI_action;
    private Image actionImage;
    private Coroutine doing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionImage = interactionUI_action.GetComponent<Image>();
        actionImage.fillAmount = 0;
    }

    public void showInteraction()
    {
        interactionUI_interaction.SetActive(true);
    }

    public void hideInteraction()
    {
        interactionUI_interaction.SetActive(false);
    }

    public void showAction(float time)
    {
        hideInteraction();
        interactionUI_action.SetActive(true);

        if (doing != null)
        {
            StopCoroutine(doing);
        }

        doing = StartCoroutine(Doing(time));
    }

    public void hideAction()
    {
        interactionUI_interaction.SetActive(false);
        actionImage.fillAmount = 0;
    }

    IEnumerator Doing(float time)
    {
        float elapsedTime = 0;
        actionImage.fillAmount = 0;
        while (elapsedTime < time)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            actionImage.fillAmount = elapsedTime / time;
        }
        hideAction();
        showInteraction();
    }


}
