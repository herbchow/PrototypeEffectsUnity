using UnityEngine;
using System.Collections;

public class FullScreenGodrayAlt : MonoBehaviour
{
    public Shader ItemMaskShader;
    private RenderTexture MaskRt;
    private GameObject _godrayCamera;

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
    }

    private void OnPreRender()
    {
        var cam = GodrayCamera.camera;
        cam.targetTexture = MaskRt;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.red;
        cam.RenderWithShader(ItemMaskShader, "RenderType");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(MaskRt, destination);
    }
}
