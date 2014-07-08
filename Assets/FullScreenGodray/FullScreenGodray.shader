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
        fClamp ("fClamp", Float) = 1.0
		vHalfPixel ("vHalfPixel", Vector) = (0,0,0,0)
		_AlphaCutoff ("Base Alpha cutoff", Range (0,.9)) = .5
    }
    SubShader {
		Pass{		
				AlphaTest Greater [_AlphaCutoff]        
		        CGPROGRAM
		        #pragma target 3.0
				#pragma vertex vert
		        #pragma fragment frag

				#include "UnityCG.cginc"
		 
				uniform sampler2D _MainTex;
		        uniform sampler2D tItemMask;
				uniform sampler2D tLightSource;
		        uniform float fX,fY,fExposure,fDecay,fDensity,fClamp;
				uniform float4 vHalfPixel;
				uniform float4 _MainTex_TexelSize;
		 
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

				float4 flippedSampleLight(float2 uv)
				{
					float2 modified = uv;
					#if UNITY_UV_STARTS_AT_TOP
						if(_MainTex_TexelSize.y < 0)
							modified.y = 1-modified.y;
					#endif

					return tex2D(tLightSource, modified);
				}
		 
		        half4 frag (v2f i) : COLOR
		        {
					int iSamples=20;

					float2 uv = i.uv - vHalfPixel;
					float2 flippedYUv = uv;
					float flippedFy = fY;
					#if UNITY_UV_STARTS_AT_TOP
						if(_MainTex_TexelSize.y < 0)
						{
							flippedYUv.y = 1-flippedYUv.y;
							flippedFy = 1-flippedFy;
						}
					#endif

					float2 deltaTextCoord = float2(uv - float2(fX,flippedFy));
					deltaTextCoord *= 1.0 /  float(iSamples) * fDensity;

					float illuminationDecay = 1.0;
					half4 sceneColor = tex2D(_MainTex, uv);
					half4 itemMask = tex2D(tItemMask, flippedYUv);
					float2 coord = uv;

					for(int i=0; i < iSamples ; i++)
					{
						coord -= deltaTextCoord;
					    float4 lightSource = flippedSampleLight(coord);
						float4 lightContribution = lightSource * fExposure * illuminationDecay *itemMask.a;
						float4 sceneSample = tex2D(_MainTex, coord);
					    sceneColor += sceneSample*lightContribution*sceneSample.a;
					    illuminationDecay *= fDecay;
					}
					sceneColor = clamp(sceneColor, 0.0, fClamp);
					return sceneColor;
		        }
		        ENDCG
				}
    } 
    Fallback off
}
