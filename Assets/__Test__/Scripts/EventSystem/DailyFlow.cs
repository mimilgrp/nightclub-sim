using System;
using TMPro;
using UnityEngine;

public class DailyFlow : MonoBehaviour
{
    public void Preparation()
    {
        Debug.Log("DailyFlow: Preparation");
    }

    public void Showing()
    {
        Debug.Log("DailyFlow: Showing");
    }

    public void Closing()
    {
        Debug.Log("DailyFlow: Closing");
    }

    public enum Shift
    {
        Preparation,
        Showing,
        Closing
    }
}
