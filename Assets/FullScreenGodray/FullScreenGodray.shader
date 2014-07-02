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
					half4 c = tex2D(tLightSource,i.uv);
					return c;             
		        }
		        ENDCG
				}
    } 
    Fallback off
}
