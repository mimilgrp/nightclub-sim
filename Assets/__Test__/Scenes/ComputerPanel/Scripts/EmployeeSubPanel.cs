using UnityEngine;

public class EmployeeSubPanel : MonoBehaviour
{
    public GameObject employeeSubPanel;

    void Start()
    {
        employeeSubPanel.SetActive(false);
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
