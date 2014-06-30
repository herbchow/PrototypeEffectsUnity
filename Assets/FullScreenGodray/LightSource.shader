Shader "Custom/LightSource" {
Properties {
		_LightSourceTex ("Light Source", 2D) = "white" {}
		_LightScreenPos ("Light Screen Position", Vector) = (0,0,0,0)
		_UvOffset ("UV Offset", Vector) = (0,0,0,0)
		_LightSize ("Light size", Float) = 2.0
			}
	SubShader {
            Tags { "Godray" = "Lightsource" }
            Pass {

			    CGPROGRAM
			    #pragma vertex vert
			    #pragma fragment frag

			    #include "UnityCG.cginc"

    			uniform float2 _LightScreenPos;
                uniform float2 _UvOffset;
    			uniform sampler2D _MainTex;
				uniform sampler2D _LightSourceTex;
                uniform float _LightSize;

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

			    float4 frag(v2f i) : COLOR
			    {
                    half4 color = half4(0,0,0,1);
                    // Find light position in world and map it to screen space
                    float2 coord = 0.5f - (i.uv - _LightScreenPos) / _LightSize * 0.5f;
                    color += pow(tex2D(_LightSourceTex,coord),2) * 2;
					return color;
			    }

			    ENDCG
		}


	} 
	FallBack "Diffuse"
}
