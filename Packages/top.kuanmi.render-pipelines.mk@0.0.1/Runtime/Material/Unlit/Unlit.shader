Shader "MKRP/Unlit"
{
    Properties
    {
		_UnlitColor("Color", Color)= (1.0, 1.0, 1.0, 1.0)
        _MainTex("MainTex",2D) = "white"{}
    }

    HLSLINCLUDE
    
    #pragma target 4.5
    #pragma only_renderers d3d11 playstation xboxone xboxseries vulkan metal switch
    
    //-------------------------------------------------------------------------------------
    // Variant
    //-------------------------------------------------------------------------------------

    //-------------------------------------------------------------------------------------
    // Define
    //-------------------------------------------------------------------------------------

    //-------------------------------------------------------------------------------------
    // Include
    //-------------------------------------------------------------------------------------

    
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/top.kuanmi.render-pipelines.mk/Runtime/ShaderLibrary/ShaderVariables.hlsl"
    
    //#include "UnityCG.cginc"

    //-------------------------------------------------------------------------------------
    // variable declaration
    //-------------------------------------------------------------------------------------

    #include "Packages/top.kuanmi.render-pipelines.mk/Runtime/Material/Unlit/UnlitProperties.hlsl"
     
    ENDHLSL
    SubShader
    {
        Tags
        {
            "Queue" = "Geometry"
        }
        LOD 100
        Pass
        {
            Tags
            {
                "LightMode" = "SRPDefaultUnlit"
            }
            HLSLPROGRAM

            #include "Packages/top.kuanmi.render-pipelines.mk/Runtime/Material/Unlit/Unlit.hlsl"
            
            #pragma vertex Vert
            #pragma fragment Frag
            ENDHLSL
        }
    }
}