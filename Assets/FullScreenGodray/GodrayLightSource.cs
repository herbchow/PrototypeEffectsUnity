using UnityEngine;

public class GodrayLightSource : MonoBehaviour
{
    public GameObject LitProduct;
    private FullScreenGodray FullScreenGodray;
	// Use this for initialization
	void Start ()
	{
	    FullScreenGodray = Camera.main.GetComponent<FullScreenGodray>();
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
