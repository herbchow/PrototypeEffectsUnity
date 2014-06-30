Shader "Custom/LightSourceTagged" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Godray"="LightSource" }
		Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater 0
		SetTexture [_MainTex] {combine one-texture,texture}
		}
	} 
	FallBack "Diffuse"
}
