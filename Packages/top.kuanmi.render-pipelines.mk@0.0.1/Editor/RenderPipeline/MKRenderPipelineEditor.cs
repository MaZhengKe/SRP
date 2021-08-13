using KuanMi.Rendering.MKRP;
using UnityEditor;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    [CustomEditor(typeof(MKRenderPipelineAsset))]
    [CanEditMultipleObjects]
    sealed class MKRenderPipelineEditor : UnityEditor.Editor
    {
        SerializedMKRenderPipelineAsset m_SerializedMKRenderPipeline;
        
        void OnEnable()
        {
            m_SerializedMKRenderPipeline = new SerializedMKRenderPipelineAsset(serializedObject);
        }

        public override void OnInspectorGUI()
        {
            var serialized = m_SerializedMKRenderPipeline;

            serialized.Update();
            
            MKRenderPipelineUI.Inspector.Draw(serialized, this);
            
            serialized.Apply();
        }
    }
}