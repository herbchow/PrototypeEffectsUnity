Shader "Custom/LightSourceTagged" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Godray"="LightSource" }
		Pass {
		ZWrite Off
		Blend One One
		//SetTexture [_MainTex] {combine texture}
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
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
			// On D3D when AA is used, the main texture and scene depth texture
			// will come out in different vertical orientations.
			// So flip sampling of the texture when that is the case (main texture
			// texel size will have negative Y).
			
			//#if UNITY_UV_STARTS_AT_TOP
			//if (_MainTex_TexelSize.y < 0)
			//        i.uv.y = 1-i.uv.y;
			//#endif
			//i.uv.y = 1-i.uv.y;
			half4 sceneColor = tex2D(_MainTex,i.uv);
			return sceneColor;
		}
		ENDCG
		}
	} 
	FallBack "Diffuse"
}
