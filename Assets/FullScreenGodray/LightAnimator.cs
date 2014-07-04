using UnityEngine;

public class LightAnimator : MonoBehaviour
{
    public GodrayLightSource GodrayLightSource;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    // Every frame, read the sqt from the this gameobject and write it to the light source position
	    GodrayLightSource.transform.position = gameObject.transform.position;
	    GodrayLightSource.transform.localScale = gameObject.transform.localScale;
        GodrayLightSource.transform.localRotation = gameObject.transform.localRotation;
	}
}
