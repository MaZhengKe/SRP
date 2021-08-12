using System.IO;
using KuanMi.Rendering.MKRP;
using UnityEditor;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    static class AssetFactory
    {
        class DoCreateNewAssetMKRenderPipeline : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var newAsset = CreateInstance<MKRenderPipelineAsset>();
                newAsset.name = Path.GetFileName(pathName);
                // Load default renderPipelineResources / Material / Shader
                //newAsset.renderPipelineResources = AssetDatabase.LoadAssetAtPath<RenderPipelineResources>(s_RenderPipelineResourcesPath);
                //EditorDefaultSettings.GetOrAssignDefaultVolumeProfile(newAsset);

                //as we must init the editor resources with lazy init, it is not required here

                AssetDatabase.CreateAsset(newAsset, pathName);
                ProjectWindowUtil.ShowCreatedAsset(newAsset);
            }
        }
        
        [MenuItem("Assets/Create/Rendering/MK Render Pipeline Asset" , priority = 1)]
        static void CreateMKRenderPipeline()
        {
            var icon = EditorGUIUtility.FindTexture("ScriptableObject Icon");
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<DoCreateNewAssetMKRenderPipeline>(), "New MKRenderPipelineAsset.asset", icon, null);
        }
    }
}