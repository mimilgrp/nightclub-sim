using UnityEngine;
using UnityEngine.UI;

public class OptionListener : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float minFOV = 10f;
    public float maxFOV = 30f;

    private Camera mainCam;
    private GrayWhiteView grayEffect;

    private void Start()
    {
        // Récupère la Main Camera et l'effet de vue si présent
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("Main Camera not found.");
            return;
        }

        grayEffect = mainCam.GetComponent<GrayWhiteView>();

        // Trouve automatiquement les composants enfants
        Slider zoomSlider = transform.Find("Zoom/ZoomSlider")?.GetComponent<Slider>();
        Button exitButton = transform.Find("ExitButton")?.GetComponent<Button>();
        Toggle viewToggle = transform.Find("ViewType/Toggle")?.GetComponent<Toggle>();

        // Configure le ZoomSlider
        if (zoomSlider != null)
        {
            zoomSlider.onValueChanged.AddListener(UpdateZoom);
        }

        // Configure le Toggle de vue grise
        if (viewToggle != null)
        {
            viewToggle.onValueChanged.AddListener(SetGrayEffect);
        }
    }

    private void UpdateZoom(float value)
    {
        if (mainCam != null)
        {
            float fov = Mathf.Lerp(maxFOV, minFOV, value);
            mainCam.fieldOfView = fov;
            Debug.Log("Zoom appliqué : FOV = " + fov);
        }
    }

    private void SetGrayEffect(bool enabled)
    {
        if (grayEffect != null)
        {
            grayEffect.enabled = enabled;
            Debug.Log("Grayscale: " + enabled);
        }
    }
}
