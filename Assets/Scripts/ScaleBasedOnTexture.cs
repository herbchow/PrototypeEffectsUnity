using UnityEngine;

public class ScaleBasedOnTexture : MonoBehaviour
{
    public Texture2D Texture;
    public float BaseXScale = 1f;
    // Use this for initialization
    private void Awake()
    {
        gameObject.renderer.material.mainTexture = Texture;
        Debug.Log(string.Format("{0}, {1}", Texture.width, Texture.height));
        var heightRatio = Texture.height/(float) Texture.width;
        gameObject.transform.localScale = new Vector3(BaseXScale, BaseXScale*heightRatio, 1f);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
