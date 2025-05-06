using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    private Camera mainCam;
    private GreyWhiteView greyWhiteView;

    public Slider zoomSlider;
    public Toggle viewToggle;

    [Header("Zoom Settings")]
    public float minFOV = 10f;
    public float maxFOV = 30f;

    private void Start()
    {
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("OptionPanel: Main camera not found");
            return;
        }

        greyWhiteView = mainCam.GetComponent<GreyWhiteView>();

        if (zoomSlider != null)
        {
            zoomSlider.value = 1f - (mainCam.fieldOfView - minFOV) / (maxFOV - minFOV);
            zoomSlider.onValueChanged.AddListener(SetZoom);
        }

        if (viewToggle != null)
        {
            viewToggle.isOn = greyWhiteView.enabled;
            viewToggle.onValueChanged.AddListener(SetGreyWhiteView);
        }
    }

    private void SetZoom(float value)
    {
        if (mainCam != null)
        {
            float fov = Mathf.Lerp(maxFOV, minFOV, value);
            mainCam.fieldOfView = fov;
            Debug.Log("OptionPanel: Zoom " + fov);
        }
    }

    private void SetGreyWhiteView(bool enabled)
    {
        if (greyWhiteView != null)
        {
            greyWhiteView.enabled = enabled;
            Debug.Log("OptionPanel: GreyWhite view " + enabled);
        }
    }
}
