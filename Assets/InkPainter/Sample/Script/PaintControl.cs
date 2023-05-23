using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintControl : MonoBehaviour
{
    public GameObject paintedImg;
    public Texture pTexture;
    int paintedPixelCount = 0;

    void Start()
    {
        pTexture = paintedImg.GetComponent<MeshRenderer>().material.mainTexture;
    }

    public void Update()
    {
        Color32[] pixels = ((Texture2D)(pTexture)).GetPixels32();

        for (int i = 0; i < pixels.Length; i++)
        {
    
            if (pixels[i].r > 220 )
            {
                paintedPixelCount++;
            }
        }

        float paintedPercentage = ((float)paintedPixelCount / pixels.Length) * 100f;
        Debug.Log(paintedPixelCount);
    }
}
