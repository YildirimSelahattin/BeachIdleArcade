using UnityEngine;

public class Paintable : MonoBehaviour
{
    const int TEXTURE_SIZE = 100;
    public static Paintable Instance;
    public float extendsIslandOffset = 1;

    RenderTexture extendIslandsRenderTexture;
    RenderTexture uvIslandsRenderTexture;
    RenderTexture maskRenderTexture;
    RenderTexture supportTexture;

    Renderer rend;

    int maskTextureID = Shader.PropertyToID("_MaskTexture");

    public RenderTexture getMask() => maskRenderTexture;
    public RenderTexture getUVIslands() => uvIslandsRenderTexture;
    public RenderTexture getExtend() => extendIslandsRenderTexture;
    public RenderTexture getSupport() => supportTexture;
    public Renderer getRenderer() => rend;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        maskRenderTexture.filterMode = FilterMode.Bilinear;

        extendIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        extendIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        uvIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        uvIslandsRenderTexture.filterMode = FilterMode.Bilinear;

        supportTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0);
        supportTexture.filterMode = FilterMode.Bilinear;

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandsRenderTexture);

        PaintManager.instance.initTextures(this);
    }

    void OnDisable()
    {
        maskRenderTexture.Release();
        uvIslandsRenderTexture.Release();
        extendIslandsRenderTexture.Release();
        supportTexture.Release();
    }

    public Texture pTexture;
    int paintedPixelCount = 0;
    public Color32[] pixels;
    float paintedPercentage;

    public void OnClickDebug()
    {
        paintedPixelCount = 0;
        if (maskRenderTexture != null)
        {
            // Create a new Texture2D and copy the pixel data from the RenderTexture
            Texture2D convertedTexture = new Texture2D(maskRenderTexture.width, maskRenderTexture.height);
            RenderTexture.active = maskRenderTexture;
            convertedTexture.ReadPixels(new Rect(0, 0, maskRenderTexture.width, maskRenderTexture.height), 0, 0);
            convertedTexture.Apply();

            pTexture = convertedTexture;
        }
        else
        {
            Debug.LogError("The mainTexture of the material is not a RenderTexture.");
        }

        pixels = ((Texture2D)(pTexture)).GetPixels32();

        for (int i = 0; i < pixels.Length; i++)
        {
            //Debug.Log(pixels[i].r);
            if (pixels[i].b > 10)
            {
                paintedPixelCount++;
            }
        }

        paintedPercentage = ((float)paintedPixelCount / pixels.Length) * 100f;
        Debug.Log(paintedPixelCount);
        Debug.Log(paintedPercentage);

        if(paintedPercentage >= 5)
        {
            PlayerManager.Instance.creamingDoneButton.SetActive(true);
        }
    }
}