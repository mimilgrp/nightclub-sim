using UnityEngine;

public class DJAnimation : MonoBehaviour
{
    Animator animator;

    public int delayBeforeShowingTime = 3600;
    public int delayAfterClosingTime = 0;

    private float gameTime;
    private int showingTime;
    private int closingTime;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (TimeManager.Instance != null)
        {
            gameTime = TimeManager.Instance.gameTime;
            showingTime = TimeManager.Instance.showingTime;
            closingTime = TimeManager.Instance.closingTime;
        } 

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
