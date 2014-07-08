using UnityEngine;

public class FullScreenGodray : MonoBehaviour
{
    public Shader ItemMaskShader;
    public Shader LightSourceShader;
    public float LightScreenCoordX;
    public float LightScreenCoordY;

    public Material GodrayMaterial;
    public RenderTexture MaskRt;
    public RenderTexture LightSourceRt;

    private GameObject _godrayCamera;
    private const string LayerGodrayProducts = "Product";
    private const string LayerGodrayLight = "GodrayLight";
    
    private GameObject GodrayCamera
    {
        get
        {
            if (_godrayCamera == null)
            {
                _godrayCamera = new GameObject("Godray Camera");
                _godrayCamera.AddComponent<Camera>();
                _godrayCamera.camera.CopyFrom(Camera.main);
                _godrayCamera.camera.depth -= 1;
            }
            return _godrayCamera;
        }
    }

    // Use this for initialization
    private void Start()
    {
        const int downscale = 1;
        var rtWidth = Screen.width/downscale;
        var rtHeight = Screen.height/downscale;
        MaskRt = new RenderTexture(rtWidth, rtHeight,16);
        LightSourceRt = new RenderTexture(rtWidth, rtHeight,16);
    }

    private void OnPreRender()
    {
        var cam = GodrayCamera.camera;
        var saveMask = cam.cullingMask;
        cam.targetTexture = MaskRt;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.cullingMask = 1 << LayerMask.NameToLayer(LayerGodrayProducts);
        cam.RenderWithShader(ItemMaskShader, "RenderType");
        cam.targetTexture = LightSourceRt;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.black;
        cam.cullingMask = 1 << LayerMask.NameToLayer(LayerGodrayLight);
        cam.RenderWithShader(LightSourceShader, "Godray");
        cam.cullingMask = saveMask;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        GodrayMaterial.SetTexture("tItemMask", MaskRt);
        GodrayMaterial.SetTexture("tLightSource", LightSourceRt);
        GodrayMaterial.SetFloat("fX", LightScreenCoordX);
        GodrayMaterial.SetFloat("fY", LightScreenCoordY);
        Graphics.Blit(src, dst, GodrayMaterial);
    }
}
