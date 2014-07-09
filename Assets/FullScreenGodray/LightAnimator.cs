using UnityEngine;

public class LightAnimator : MonoBehaviour
{
    private GodrayLightSource _godrayLightSource;
	// Use this for initialization
	void Start ()
	{
	    _godrayLightSource = Object.FindObjectOfType<GodrayLightSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    // Every frame, read the sqt from the this gameobject and write it to the light source position
	    _godrayLightSource.transform.position = gameObject.transform.position;
	    _godrayLightSource.transform.localScale = gameObject.transform.localScale;
        _godrayLightSource.transform.localRotation = gameObject.transform.localRotation;
	}

    public void ProductSelectedAnimComplete()
    {
        Debug.Log("ProductSelected anim complete");
    }
}
