using System.Collections;
using UnityEngine;

public class CustomerReaction : MonoBehaviour
{
    public GameObject dollars;
    public GameObject waitingAngry;

    public void ShowMoney()
    {
        StartCoroutine(ShowTemporary(dollars, 2f));
    }

    public void ShowAngryWaitingReaction()
    {
        StartCoroutine(ShowTemporary(waitingAngry, 2f));
    }
    private IEnumerator ShowTemporary(GameObject obj, float duration)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}
