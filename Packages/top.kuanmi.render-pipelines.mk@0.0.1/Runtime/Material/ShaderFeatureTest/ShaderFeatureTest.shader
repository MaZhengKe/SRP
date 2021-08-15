Shader "MKRP/ShaderFeatureTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [ToggleUI] _ShowTex("ShowTex", Float) = 0.0
        [ToggleUI] _IsRed("IsRed", Float) = 0.0
    }

    HLSLINCLUDE
    #pragma multi_compile MKRP_SHOW_TEX
    #pragma multi_compile __ TEST_ON
    ENDHLSL

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }
            CGPROGRAM

            //#pragma multi_compile_local __ _MKRP_SHOW_TEX
            #pragma shader_feature_local _MKRP_SHOW_TEX
            #pragma shader_feature_local _RED
            
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                //#define _MKRP_SHOW_TEX
#ifdef _MKRP_SHOW_TEX
                return col;
#else
    #ifdef _RED
                return fixed4(1.0, 0.0, 0, 1);
    #else
                return fixed4(0.0, 1.0, 0, 1);
    #endif
                
#endif
            }
            ENDCG
        }
    }
    
    CustomEditor "KuanMiEditor.Rendering.MKRP.ShaderFeatureTestGUI"
}