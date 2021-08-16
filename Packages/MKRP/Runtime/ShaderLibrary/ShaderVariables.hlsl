#ifndef UNITY_SHADER_VARIABLES_INCLUDED_MK
#define UNITY_SHADER_VARIABLES_INCLUDED_MK

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

CBUFFER_START(UnityPerDraw)
float4x4 unity_ObjectToWorld;
float4x4 unity_WorldToObject;
float4 unity_LODFade;
real4 unity_WorldTransformParams;
float4x4 unity_MatrixV;
float4x4 glstate_matrix_projection;
CBUFFER_END

float4x4 unity_MatrixVP;

#endif