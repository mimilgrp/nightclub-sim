using UnityEngine;

public class EmployeeSubPanel : MonoBehaviour
{
    public GameObject employeeSubPanel;

    private Transform beveragesSpawn;

    public void OpenEmployeeSubPanel()
    {
        employeeSubPanel.SetActive(true);
    }

    public void ExitEmployeeSubPanel()
    {
        employeeSubPanel.SetActive(false);
    }
}
