Shader "Custom/FullScreenGodray" {
Properties 
    {
		_MainTex ("Main Texture (RGB)", 2D) = "white" {}
        tItemMask ("Item Mask (RGB)", 2D) = "white" {}
		tLightSource ("Light Source (RGB)", 2D) = "wite" {}
        fX ("fX", Float) = 0.5 // you can feed mouse xpos here with script: var mpos:Vector3 = Camera.main.ScreenToViewportPoint(Input.mousePosition); renderer.material.SetFloat( "fX", mpos.x);
        fY ("fY", Float) = 0.5 //  mouse ypos
        fExposure ("fExposure", Float) = 0.6
        fDecay ("fDecay", Float) = 0.93
        fDensity ("fDensity", Float) = 0.96
        fWeight ("fWeight", Float) = 0.4
        fClamp ("fClamp", Float) = 1.0
		vHalfPixel ("vHalfPixel", Vector) = (0,0,0,0)
    }
    SubShader {
		Pass{		         
		        CGPROGRAM
		        #pragma target 3.0
				#pragma vertex vert
		        #pragma fragment frag

				#include "UnityCG.cginc"
		 
				uniform sampler2D _MainTex;
		        uniform sampler2D tItemMask;
				uniform sampler2D tLightSource;
		        uniform float fX,fY,fExposure,fDecay,fDensity,fWeight,fClamp;
				uniform float4 vHalfPixel;
		 
		        struct v2f {
					float4 pos : POSITION;
		            float2 uv : TEXCOORD0;
		        };

				v2f vert( appdata_img v )
				{
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = v.texcoord;
					return o;
				}
		 
		        half4 frag (v2f i) : COLOR
		        {
					int iSamples=60;
					
					float2 uv = i.uv - vHalfPixel;
					float2 deltaTextCoord = float2(uv - float2(fX,fY));

					deltaTextCoord *= 1.0 /  float(iSamples) * fDensity;
					float illuminationDecay = 1.0;
					half4 sceneColor = tex2D(_MainTex, uv);
					half4 itemMask = tex2D(tItemMask, uv);
					float2 coord = uv;
					
					for(int i=0; i < iSamples ; i++)
					{
					    coord -= deltaTextCoord;
					    float4 lightSource = tex2D(tLightSource, coord);
						float4 lightContribution = lightSource * fExposure * illuminationDecay;
						float4 sceneSample = tex2D(_MainTex, coord);
					    sceneColor += sceneSample*lightContribution*sceneSample.a;
					    illuminationDecay *= fDecay;
					}
					//FragColor *= fExposure;
					sceneColor = clamp(sceneColor, 0.0, fClamp);
					return sceneColor;
		        }
		        ENDCG
				}
    } 
    Fallback off
}
