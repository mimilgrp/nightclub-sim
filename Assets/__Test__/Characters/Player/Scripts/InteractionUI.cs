using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public GameObject description;
    public GameObject interaction;
    public GameObject loading;
    public GameObject NoCustomer;
    public GameObject NoBeverage;

    private TextMeshProUGUI descriptionText;
    private Image loadingImage;

    private Coroutine loadingCoroutine;

    void Start()
    {
        descriptionText = description.GetComponent<TextMeshProUGUI>();
        descriptionText.text = null;

        loadingImage = loading.GetComponent<Image>();
        loadingImage.fillAmount = 0;
    }
    public void ShowNoCustomerReaction()
    {
        StartCoroutine(ShowTemporary(NoCustomer, 2f));
    }
    public void ShowNoBeverageReaction()
    {
        StartCoroutine(ShowTemporary(NoBeverage, 2f));
    }

    private void ShowDescription(string text)
    {
        description.SetActive(true);
        descriptionText.text = text.ToUpper();
    }

    private void HideDescription()
    {
        description.SetActive(false);
        descriptionText.text = null;
    }

    public void ShowInteraction(string description)
    {
        interaction.SetActive(true);
        ShowDescription(description);
    }

    public void HideInteraction()
    {
        interaction.SetActive(false);
        HideDescription();
    }

    public void ShowLoading(float time)
    {
        interaction.SetActive(false);
        loading.SetActive(true);

        if (loadingCoroutine != null)
        {
            StopCoroutine(loadingCoroutine);
        }

        loadingCoroutine = StartCoroutine(Loading(time));
    }

    private void HideLoading()
    {
        loading.SetActive(false);
        loadingImage.fillAmount = 0;
    }

    IEnumerator Loading(float time)
    {
        float elapsedTime = 0;
        loadingImage.fillAmount = 0;

        while (elapsedTime < time)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            loadingImage.fillAmount = elapsedTime / time;
        }

        HideLoading();
    }

    private IEnumerator ShowTemporary(GameObject obj, float duration)
    {
        descriptionText.text = null;
        obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}
