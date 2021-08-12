using KuanMi.Rendering.MKRP;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    public class SerializedRenderPipelineSettings
    {
        public SerializedProperty root;
        
        public SerializedProperty colorBufferFormat;

        public SerializedRenderPipelineSettings(SerializedProperty root)
        {
            this.root = root;
            colorBufferFormat               = root.Find((RenderPipelineSettings s) => s.colorBufferFormat);
        }
    }
}