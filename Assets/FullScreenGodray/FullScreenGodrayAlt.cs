using UnityEngine;
using System.Collections;

public class FullScreenGodrayAlt : MonoBehaviour
{
    public Shader ItemMaskShader;
    public Shader LightSourceShader;
    public Shader GodrayShader;
    public Texture2D LightSource;

    private Material _godrayMaterial;
    public Material GodrayMaterial
    {
        get
        {
            if (_godrayMaterial == null)
            {
                _godrayMaterial = new Material(GodrayShader);
            }
            ExposeGodrayMaterial = _godrayMaterial;
            return _godrayMaterial;
        }
    }

    public Material ExposeGodrayMaterial;

    public RenderTexture MaskRt;
    public RenderTexture LightSourceRt;
    private GameObject _godrayCamera;
    private Material _lightSourceMaterial;

    private GameObject GodrayCamera
    {
        get
        {
            if (_godrayCamera == null)
            {
                _godrayCamera = new GameObject("Godray Camera");
                _godrayCamera.AddComponent<Camera>();
                _godrayCamera.camera.CopyFrom(Camera.main);
            }
            return _godrayCamera;
        }
    }

    // Use this for initialization
    private void Start()
    {
        var rtWidth = Screen.width;
        var rtHeight = Screen.height;
        MaskRt = new RenderTexture(rtWidth, rtHeight, 16);
        LightSourceRt = new RenderTexture(rtWidth, rtHeight, 16);
    }

    private void OnPreRender()
    {
        var cam = GodrayCamera.camera;
        cam.targetTexture = MaskRt;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0, 0, 0, 0);
        cam.RenderWithShader(ItemMaskShader, "RenderType");
        cam.targetTexture = LightSourceRt;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.black;
        cam.RenderWithShader(LightSourceShader, "Godray");
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        GodrayMaterial.SetTexture("tItemMask", MaskRt);
        GodrayMaterial.SetTexture("tLightSource", LightSourceRt);
        //ImageEffects.BlitWithMaterial(GodrayMaterial, src, dst);
        Graphics.Blit(src, dst,GodrayMaterial);
    }
}
