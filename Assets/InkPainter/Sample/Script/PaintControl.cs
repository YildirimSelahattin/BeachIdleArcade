using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintControl : MonoBehaviour
{
    public GameObject paintedImg;
    public Texture pTexture;
    int paintedPixelCount = 0;
    public Color32[] pixels;

    public void OnClickDebug()
    {
        MeshRenderer meshRenderer = paintedImg.GetComponent<MeshRenderer>();
        if (meshRenderer != null && meshRenderer.material != null)
        {
            RenderTexture renderTexture = meshRenderer.material.mainTexture as RenderTexture;
            if (renderTexture != null)
            {
                // Create a new Texture2D and copy the pixel data from the RenderTexture
                Texture2D convertedTexture = new Texture2D(renderTexture.width, renderTexture.height);
                RenderTexture.active = renderTexture;
                convertedTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                convertedTexture.Apply();

                pTexture = convertedTexture;
            }
            else
            {
                Debug.LogError("The mainTexture of the material is not a RenderTexture.");
            }
        }
        else
        {
            Debug.LogError("Missing MeshRenderer or material on the paintedImg GameObject.");
        }


        pixels = ((Texture2D)(pTexture)).GetPixels32();

        for (int i = 0; i < pixels.Length; i++)
        {
            //Debug.Log(pixels[i].r);
            if (pixels[i].b < 250)
            {
                paintedPixelCount++;
            }
        }

        float paintedPercentage = ((float)paintedPixelCount / pixels.Length) * 100f;
        Debug.Log(paintedPixelCount);
    }
}
