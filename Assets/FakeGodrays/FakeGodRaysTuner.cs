using UnityEngine;

namespace Assets.FakeGodrays
{
    public class FakeGodRaysTuner : MonoBehaviour
    {
        private float X = 0.5f;
        private float Y = 0.5f;
        private float Density = 0.2f;
        private float Decay = 0.8f;
        private float Exposure = 1f;
        private float Clamp = 10f;
        private float Weight = 0.8f;

        private void OnGUI()
        {
            int xMargin = 25;
            int width = 200;
            int xMarginText = xMargin + width;
            int height = 80;
            float minValue = -5f;
            float maxValue = 5f;
            int currYPos = 0;
            currYPos += height;
            GUI.Label(new Rect(xMarginText, currYPos, width, height), "X: " + X.ToString());
            X = GUI.HorizontalSlider(new Rect(xMargin, currYPos, width, height), X, 0, 1);
            currYPos += height;
            GUI.Label(new Rect(xMarginText, currYPos, width, height), "Y: " + Y.ToString());
            Y = GUI.HorizontalSlider(new Rect(xMargin, currYPos, width, height), Y, 0, 1);
            currYPos += height;
            GUI.Label(new Rect(xMarginText, currYPos, width, height), "Density: " + Density.ToString());
            Density = GUI.HorizontalSlider(new Rect(xMargin, currYPos, width, height),
                                           Density,
                                           minValue,
                                           maxValue);
            currYPos += height;
            GUI.Label(new Rect(xMarginText, currYPos, width, height), "Decay: " + Decay.ToString());
            Decay = GUI.HorizontalSlider(new Rect(xMargin, currYPos, width, height), Decay, minValue, maxValue);
            currYPos += height;
            GUI.Label(new Rect(xMarginText, currYPos, width, height), "Exposure: " + Exposure.ToString());
            Exposure = GUI.HorizontalSlider(new Rect(xMargin, currYPos, width, height),
                                            Exposure,
                                            minValue,
                                            maxValue);
            currYPos += height;
            GUI.Label(new Rect(xMarginText, currYPos, width, height), "Clamp: " + Clamp.ToString());
            Clamp = GUI.HorizontalSlider(new Rect(xMargin, currYPos, width, height), Clamp, -10, 10);
            currYPos += height;
            GUI.Label(new Rect(xMarginText, currYPos, width, height), "Weight: " + Weight.ToString());
            Weight = GUI.HorizontalSlider(new Rect(xMargin, currYPos, width, height),
                                          Weight,
                                          minValue,
                                          maxValue);
        }

        public void SetParameters(Material mat)
        {
            mat.SetFloat("fX", X);
            mat.SetFloat("fY", Y);
            mat.SetFloat("fDensity", Density);
            mat.SetFloat("fDecay", Decay);
            mat.SetFloat("fExposure", Exposure);
            mat.SetFloat("fClamp", Clamp);
            mat.SetFloat("fWeight", Weight);
        }
    }
}
