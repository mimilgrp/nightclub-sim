using UnityEngine;

public class ComputerPanel : MonoBehaviour
{
    public GameObject shopSubPanel;
    public GameObject employeeSubPanel;

    void Start()
    {
        shopSubPanel.SetActive(false);
        employeeSubPanel.SetActive(false);
    }

    public void OpenShopSubPanel()
    {
        shopSubPanel.SetActive(true);
    }

    public void ExitShopSubPanel()
    {
        shopSubPanel.SetActive(false);
    }

    public void OpenEmployeeSubPanel()
    {
        employeeSubPanel.SetActive(true);
    }

    public void ExitEmployeeSubPanel()
    {
        employeeSubPanel.SetActive(false);
    }
}
