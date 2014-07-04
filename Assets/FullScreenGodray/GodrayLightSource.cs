using UnityEngine;
using System.Collections;

public class GodrayLightSource : MonoBehaviour
{
    public GameObject LitProduct;
    public FullScreenGodrayAlt FullScreenGodray;
	// Use this for initialization
	void Start ()
	{
	    FullScreenGodray = Camera.main.GetComponent<FullScreenGodrayAlt>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        // Position drives fx,fy of the FullScreenGodray shader
	    var screenPos = Camera.main.WorldToViewportPoint(gameObject.transform.position);
	    FullScreenGodray.LightScreenCoordX = screenPos.x;
	    FullScreenGodray.LightScreenCoordY = screenPos.y;
	}
}
