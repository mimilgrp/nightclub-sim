using UnityEngine;

public class GrayWhiteView : MonoBehaviour
{

    public Material grayscaleMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (grayscaleMaterial != null)
        {
            Graphics.Blit(src, dest, grayscaleMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
