using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace KuanMi.Rendering.MKRP
{
    [Serializable]
    public class RenderPipelineSettings
    {
        public enum ColorBufferFormat
        {
            /// <summary>R11G11B10 for faster rendering.</summary>
            R11G11B10 = GraphicsFormat.B10G11R11_UFloatPack32,
            /// <summary>R16G16B16A16 for better quality.</summary>
            R16G16B16A16 = GraphicsFormat.R16G16B16A16_SFloat
        }
        
        public ColorBufferFormat colorBufferFormat;


        internal static RenderPipelineSettings NewDefault()
        {
            RenderPipelineSettings settings = new RenderPipelineSettings()
            {
                colorBufferFormat = ColorBufferFormat.R11G11B10,
            };
            return settings;
        }
    }
}