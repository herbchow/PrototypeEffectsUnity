Shader "Custom/LightSourceTagged" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Godray"="LightSource" }
		Pass {
		ZWrite Off
		Blend One One
		SetTexture [_MainTex] {combine texture}
		}
	} 
	FallBack "Diffuse"
}
