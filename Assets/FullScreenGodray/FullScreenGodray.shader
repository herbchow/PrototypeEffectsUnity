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
		        uniform float fX,fY,fExposure,fDecay,fDensity,fWeight,fClamp;
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
		 
		        half4 frag (v2f i) : COLOR
		        {
					int iSamples=10;

					// On D3D when AA is used, the main texture and scene depth texture
					// will come out in different vertical orientations.
					// So flip sampling of the texture when that is the case (main texture
					// texel size will have negative Y).
					
					//#if UNITY_UV_STARTS_AT_TOP
					//if (_MainTex_TexelSize.y < 0)
					//        i.uv.y = 1-i.uv.y;
					//#endif

					
					float2 uv = i.uv - vHalfPixel;
					float2 flippedYUv = uv;
					#if UNITY_UV_STARTS_AT_TOP
						if(_MainTex_TexelSize.y < 0)
							flippedYUv.y = 1-flippedYUv.y;
					#endif

					float2 deltaTextCoord = float2(uv - float2(fX,fY));
					float2 flippedDeltaTexCoord = float2(flippedYUv - float2(fX,-fY));

					deltaTextCoord *= 1.0 /  float(iSamples) * fDensity;
					flippedDeltaTexCoord *= 1.0 / float(iSamples) * fDensity;

					float illuminationDecay = 1.0;
					half4 sceneColor = tex2D(_MainTex, uv);
					half4 itemMask = tex2D(tItemMask, flippedYUv);
					float2 coord = uv;
					float2 flippedCoord = flippedYUv;
					
					for(int i=0; i < iSamples ; i++)
					{
						coord -= deltaTextCoord;
						flippedCoord -= flippedDeltaTexCoord;

						float2 flippedYCoord = coord;
						#if UNITY_UV_STARTS_AT_TOP
							if(_MainTex_TexelSize.y < 0)
								flippedYCoord = flippedCoord;
						#endif
					    float4 lightSource = tex2D(tLightSource, flippedYCoord);
						float4 lightContribution = lightSource * fExposure * illuminationDecay;
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
