using UnityEngine;
using System.Collections;

public class FullScreenGodray : MonoBehaviour
{
    public Shader ItemMaskShader;
    private Camera _godrayCamera;
    private GameObject _maskCamera;

    private void OnPostRender()
    {
        if (!enabled || !gameObject.active || !ItemMaskShader)
            return;
        if (_maskCamera == null)
        {
            _maskCamera = new GameObject("MaskCamera");
            _maskCamera.AddComponent<Camera>();
            _maskCamera.camera.enabled = false;
            var cam = _maskCamera.camera;
            cam.CopyFrom(camera);
            cam.backgroundColor = Color.black;
            cam.clearFlags = CameraClearFlags.SolidColor;
        }
        _maskCamera.camera.RenderWithShader(ItemMaskShader, "RenderType");
    }

    private void OnDisable()
    {
        DestroyImmediate(_maskCamera);
    }
}