using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintControl : MonoBehaviour
{
    public Texture2D texture;
    int paintedPixelCount = 0;

    public void OnClickControl()
    {
        Color32[] pixels = texture.GetPixels32();

        for (int i = 0; i < pixels.Length; i++)
        {
            // Burada, bir pikselin boyanıp boyanmadığını belirlemek için bir eşik değer kullanıyorum.
            // Bu eşik değeri, projenize bağlı olarak değiştirilebilir.
            if (pixels[i].r > 220 )
            {
                paintedPixelCount++;
            }
        }

        float paintedPercentage = ((float)paintedPixelCount / pixels.Length) * 100f;
        Debug.Log(paintedPixelCount);
    }
}
