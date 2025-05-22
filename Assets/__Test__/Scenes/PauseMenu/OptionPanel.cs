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
    public float maxFOV = 25f;

    private void Start()
    {
        mainCam = Camera.main;

        greyWhiteView = mainCam.GetComponent<GreyWhiteView>();

        zoomSlider.value = 1f - (mainCam.fieldOfView - minFOV) / (maxFOV - minFOV);
        zoomSlider.onValueChanged.AddListener(SetZoom);

        viewToggle.isOn = greyWhiteView.enabled;
        viewToggle.onValueChanged.AddListener(SetGreyWhiteView);
    }

    private void SetZoom(float value)
    {
        if (mainCam != null)
        {
            float fov = Mathf.Lerp(maxFOV, minFOV, value);
            mainCam.fieldOfView = fov;
        }
    }

    private void SetGreyWhiteView(bool enabled)
    {
        if (greyWhiteView != null)
        {
            greyWhiteView.enabled = enabled;
        }
    }
}
