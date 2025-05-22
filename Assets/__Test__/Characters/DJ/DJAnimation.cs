using UnityEngine;

public class DJAnimation : MonoBehaviour
{
    Animator animator;

    public int delayBeforeShowingTime = 3600;
    public int delayAfterClosingTime = 0;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        float gameTime = TimeManager.Instance.gameTime;
        float showingTime = DailyFlow.Instance.showingTime;
        float closingTime = DailyFlow.Instance.closingTime;

        if (gameTime > (showingTime - delayBeforeShowingTime) ||
            gameTime < (closingTime + delayAfterClosingTime))
        {
            animator.SetBool("isMixing", true);
        }
        else
        {
            animator.SetBool("isMixing", false);
        }
    }
}
