Shader "Custom/FakeGodrays" {
    Properties 
    {
        tDiffuse ("Base (RGB)", 2D) = "white" {}
        fX ("fX", Float) = 0.5 // you can feed mouse xpos here with script: var mpos:Vector3 = Camera.main.ScreenToViewportPoint(Input.mousePosition); renderer.material.SetFloat( "fX", mpos.x);
        fY ("fY", Float) = 0.5 //  mouse ypos
        fExposure ("fExposure", Float) = 0.6
        fDecay ("fDecay", Float) = 0.93
        fDensity ("fDensity", Float) = 0.96
        fWeight ("fWeight", Float) = 0.4
        fClamp ("fClamp", Float) = 1.0
        //iSamples ("iSamples", Int) = 20
    }
    SubShader {
        Tags { "Queue"="Transparent" }
        LOD 200
        Cull Off
		Blend One One
         
        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf Lambert
 
        sampler2D tDiffuse;
        float fX,fY,fExposure,fDecay,fDensity,fWeight,fClamp,iSamples;
 
        struct Input {
            float2 uvtDiffuse;
            float4 screenPos;
        };
 
        void surf (Input IN, inout SurfaceOutput o) 
        {
            int iSamples=40;

			        float2 uvCenter = float2(0.5f,0.5f);
                    // When moving away from center we will scale the uv down
                    float2 deltaFromCenter = (IN.uvtDiffuse - uvCenter)/0.5f;
                    float2 transformedUv = IN.uvtDiffuse + deltaFromCenter;

                    float2 stepMoreThanOne = step(1,transformedUv);
                    float2 stepLessThanOne = step(transformedUv,0);

					float2 vUv = transformedUv;
            //float2 vUv = IN.uvtDiffuse;
            //vUv *= float2(1,1); // repeat?
            float2 deltaTextCoord = float2(vUv - float2(fX,fY));
            deltaTextCoord *= 1.0 /  float(iSamples) * fDensity;
            float2 coord = vUv;
            float illuminationDecay = 1.0;
            float4 FragColor = float4(0,0,0,0);
            for(int i=0; i < iSamples ; i++)
            {
                coord -= deltaTextCoord;
                float4 texel = tex2D(tDiffuse, coord);
				float factor = 1;
				if(coord.x > 1)
					factor = 0;
				if(coord.x < 0)
					factor = 0;
				if(coord.y > 1)
					factor = 0;
				if(coord.y < 0)
					factor = 0;
                texel *= illuminationDecay * fWeight * factor * texel.a;
                FragColor += texel;
                illuminationDecay *= fDecay;
            }
            FragColor *= fExposure;
            FragColor = clamp(FragColor, 0.0, fClamp);
            float4 c = FragColor;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
             
        }
        ENDCG
    } 
    FallBack "Diffuse"
}
