using UnityEngine;

public class GreyWhiteView : MonoBehaviour
{

    public Material greyscaleMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (greyscaleMaterial != null)
        {
            Graphics.Blit(src, dest, greyscaleMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
