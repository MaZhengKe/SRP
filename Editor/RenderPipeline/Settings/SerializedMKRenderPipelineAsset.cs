using UnityEditor;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    public class SerializedMKRenderPipelineAsset
    {
        public SerializedObject serializedObject;
        
        public SerializedProperty renderPipelineResources;
        public SerializedRenderPipelineSettings renderPipelineSettings;

        public SerializedMKRenderPipelineAsset(SerializedObject serializedObject)
        {
            this.serializedObject = serializedObject;
            
            renderPipelineResources = serializedObject.FindProperty("m_RenderPipelineResources");
            renderPipelineSettings = new SerializedRenderPipelineSettings(serializedObject.FindProperty("m_RenderPipelineSettings"));
        }
        
        public void Update()
        {
            serializedObject.Update();
        }
        
        public void Apply()
        {
            serializedObject.ApplyModifiedProperties();
        }
        
    }
}