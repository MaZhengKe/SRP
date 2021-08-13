Shader "Custom RP/GPUInstancingUnlit" {
	
	Properties {
		_BaseColor("Color", Color)= (1.0, 1.0, 1.0, 1.0)
	}
	
	SubShader {
		Pass {
			Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }
            HLSLPROGRAM
			#include "GPUUnlitPass.hlsl"
            #pragma multi_compile_instancing
			#pragma vertex UnlitPassVertex
			#pragma fragment UnlitPassFragment
			ENDHLSL
		}
	}
}