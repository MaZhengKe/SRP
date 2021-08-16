using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace KuanMi.Rendering.MKRP
{
    partial class RenderPipelineResources : ScriptableObject
    {

        [Serializable, ReloadGroup]
        public sealed class ShaderResources
        {
            // Defaults
            [Reload("Runtime/Material/Unlit/Unlit.shader")]
            public Shader defaultPS;
        }
        
        public ShaderResources shaders;
    }
}