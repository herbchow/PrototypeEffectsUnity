using UnityEngine;

public class LightSourceQuad : MonoBehaviour
{
    public Texture2D LightSource;
    private GameObject _lightSourceCamera;
    private GameObject _fullScreenQuad;
    private RenderTexture _fullScreenRt;
    private string QuadDrawLayer = "LightSourceQuad";

    public Material LightSourceMaterial;

    private GameObject LightSourceCamera
    {
        get
        {
            if (_lightSourceCamera == null)
            {
                _lightSourceCamera = new GameObject("LightSource Camera");
                _lightSourceCamera.AddComponent<Camera>();
                _lightSourceCamera.camera.CopyFrom(Camera.main);
                _lightSourceCamera.camera.depth -= 1;
                _lightSourceCamera.camera.orthographic = true;
                _lightSourceCamera.camera.targetTexture = FullScreenRt;
                _lightSourceCamera.camera.cullingMask = 1 << LayerMask.NameToLayer(QuadDrawLayer);
                _lightSourceCamera.camera.orthographicSize = 0.5f;
                _lightSourceCamera.camera.transform.position = Vector3.zero - Vector3.forward;
            }
            return _lightSourceCamera;
        }
    }

    public RenderTexture FullScreenRt
    {
        get
        {
            if (_fullScreenRt == null)
            {
                _fullScreenRt = new RenderTexture(Screen.width, Screen.height, 16);
            }
            return _fullScreenRt;
        }
    }

    // Use this for initialization
    private void Start()
    {
        // Create full screen quad
        if (_fullScreenQuad == null)
        {
            _fullScreenQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            _fullScreenQuad.name = "LighSourceFullScreenQuad";
            _fullScreenQuad.layer = LayerMask.NameToLayer(QuadDrawLayer);
            _fullScreenQuad.renderer.material = LightSourceMaterial;
            _fullScreenQuad.transform.localScale = new Vector3(LightSourceCamera.camera.aspect, 1, 1);
        }
        LightSourceCamera.layer = LayerMask.NameToLayer(QuadDrawLayer);
    }

    // Update is called once per frame
    private void Update()
    {
        // Set the positions
        LightSourceMaterial.SetTexture("_LightSourceTex", LightSource);
        //LightSource.wrapMode = TextureWrapMode.Clamp;
        //LightSourceMaterial.SetVector("_LightScreenPos", new Vector4(0.18f, 0.48f, 0, 0));
        //LightSourceMaterial.SetFloat("_LightSize", 0.33f);
    }
}
