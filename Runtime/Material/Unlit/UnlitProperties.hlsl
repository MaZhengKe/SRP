TEXTURE2D(_UnlitColorMap);
SAMPLER(sampler_UnlitColorMap);

CBUFFER_START(UnityPerMaterial)
float4  _UnlitColor;
CBUFFER_END