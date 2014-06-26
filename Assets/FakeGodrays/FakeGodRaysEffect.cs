using Assets.FakeGodrays;
using UnityEngine;

public class FakeGodRaysEffect : MonoBehaviour
{
    private GameObject _godrayQuad;
    private float _xSlider;
    private float _ySlider;
    private float _densitySlider = 0.20f;
    private float _decaySlider = 0.8f;
    private float _exposureSlider = 1f;
    private float _clampSlider = 10f;
    private float _weightSlider = 0.8f;
    private FakeGodRaysTuner Tuner;

    private const float GODRAY_OFFSET = 0.001f;

    public bool IsBehindObject = false;
    // Use this for initialization
    private void Start()
    {
        Tuner = FindObjectOfType<FakeGodRaysTuner>();
        _godrayQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _godrayQuad.layer = LayerMask.NameToLayer("TransparentFX");
        float scaleFactor = 3.0f;
        _godrayQuad.transform.localScale = new Vector3(gameObject.transform.localScale.x*scaleFactor,
                                                       gameObject.transform.localScale.y*scaleFactor,
                                                       1f);
        _godrayQuad.transform.localPosition = gameObject.transform.localPosition;
        if (!IsBehindObject)
        {
            _godrayQuad.transform.localPosition -= _godrayQuad.transform.forward * GODRAY_OFFSET;    
        }
        else
        {
            _godrayQuad.transform.localPosition += _godrayQuad.transform.forward * GODRAY_OFFSET;    
        }
        
        _godrayQuad.renderer.material = new Material(Shader.Find("Custom/FakeGodrays"));
        _godrayQuad.renderer.material.SetTexture("tDiffuse", gameObject.renderer.material.mainTexture);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Tuner == null)
        {
            _godrayQuad.renderer.material.SetFloat("fX", _xSlider);
            _godrayQuad.renderer.material.SetFloat("fY", _ySlider);
            _godrayQuad.renderer.material.SetFloat("fDensity", _densitySlider);
            _godrayQuad.renderer.material.SetFloat("fDecay", _decaySlider);
            _godrayQuad.renderer.material.SetFloat("fExposure", _exposureSlider);
            _godrayQuad.renderer.material.SetFloat("fClamp", _clampSlider);
            _godrayQuad.renderer.material.SetFloat("fWeight", _weightSlider);
        }
        else
        {
            Tuner.SetParameters(_godrayQuad.renderer.material);
        }
    }
}
