using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoneyManager: MonoBehaviour
{
    private int moneyAmount;
    void Start()
    {
        moneyAmount = 0;
    }
    public void addMoney(int m)
    {
        moneyAmount += m;
    }
    public bool enoughMoney(int m)
    {
        return (moneyAmount >= m);
    }

    public void removeMoney(int m)
    {
        moneyAmount -= m;
    }

    public void updateMoneyHUD()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "HUD")
            {
                GameObject moneyObj = GameObject.Find("Money");
                TextMeshPro moneyshowed = moneyObj.GetComponent<TextMeshPro>();
                moneyshowed.text = moneyAmount.ToString();
                // Unsubscribe after
                SceneManager.sceneLoaded -= (scene, mode) => { };
            }
        };
        
    }

    //moneyshowed.text = moneyAmount.ToString();
}
