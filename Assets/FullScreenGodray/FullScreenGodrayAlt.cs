using UnityEngine;
using System.Collections;

public class FullScreenGodrayAlt : MonoBehaviour
{
    public Shader ItemMaskShader;
    public Shader LightSourceShader;
    public Texture2D LightSource;

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
        cam.backgroundColor = Color.red;
        cam.RenderWithShader(ItemMaskShader, "RenderType");

        cam.targetTexture = LightSourceRt;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.black;
        //Shader.SetGlobalTexture("_MainTex",LightSource);
        cam.RenderWithShader(LightSourceShader, "Godray");
        //RenderLightSource();
        // Draw a quad with material
        //RenderLightSource(LightSourceMaterial, LightSourceRt);
    }

    //private void OnRenderImage(RenderTexture src, RenderTexture dst)
    //{
    //    // TODO: The src will always be the stuff rendered on The Main.Camera (where this script is attached to)
    //    RenderLightSource(src);
    //}

    //private void RenderLightSource(RenderTexture src)
    //{
    //    //var saved = RenderTexture.active;
    //    LightSourceMaterial.SetTexture("_LightSourceTex", LightSource);
    //    LightSourceMaterial.SetVector("_LightScreenPos", new Vector4(0.18f, 0.48f, 0, 0));
    //    LightSourceMaterial.SetFloat("_LightSize", 0.33f);
    //    ImageEffects.BlitWithMaterial(LightSourceMaterial, MaskRt, LightSourceRt);
    //    //RenderTexture.active = saved;
    //}

    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    Graphics.Blit(LightSourceRt, destination);
    //}
}
