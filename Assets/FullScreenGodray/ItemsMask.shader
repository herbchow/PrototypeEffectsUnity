Shader "Custom/ItemsMask" {
SubShader {
	Tags { "RenderType"="Transparent" }
	Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater 0
		SetTexture [_MainTex] {combine one-texture,texture}
	}
}
}
