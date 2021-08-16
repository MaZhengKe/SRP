using System.IO;
using KuanMi.Rendering.MKRP;
using UnityEditor;
using UnityEngine;
using UnityEditor.ProjectWindowCallback;
using UnityEngine.Rendering;

namespace KuanMiEditor.Rendering.MKRP
{
    static class AssetFactory
    {
        static string s_RenderPipelineResourcesPath
        {
            get { return MKUtils.GetMKRenderPipelinePath() + "Runtime/RenderPipelineResources/MKRenderPipelineResources.asset"; }
        }
        
        class DoCreateNewAssetMKRenderPipeline : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var newAsset = CreateInstance<MKRenderPipelineAsset>();
                newAsset.name = Path.GetFileName(pathName);
                // Load default renderPipelineResources / Material / Shader
                newAsset.renderPipelineResources = AssetDatabase.LoadAssetAtPath<RenderPipelineResources>(s_RenderPipelineResourcesPath);
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
        
        class DoCreateNewAssetMKRenderPipelineResources : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var newAsset = CreateInstance<RenderPipelineResources>();
                newAsset.name = Path.GetFileName(pathName);

                ResourceReloader.ReloadAllNullIn(newAsset, MKUtils.GetMKRenderPipelinePath());

                AssetDatabase.CreateAsset(newAsset, pathName);
                ProjectWindowUtil.ShowCreatedAsset(newAsset);
            }
        }
        // Hide: User aren't suppose to have to create it.
        [MenuItem("Assets/Create/Rendering/MK Render Pipeline Resources", priority = CoreUtils.assetCreateMenuPriority1)]
        static void CreateRenderPipelineResources()
        {
            var icon = EditorGUIUtility.FindTexture("ScriptableObject Icon");
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<DoCreateNewAssetMKRenderPipelineResources>(), "New MKRenderPipelineResources.asset", icon, null);
        }

        [MenuItem("Assets/Create/Rendering/FindPath", priority = CoreUtils.assetCreateMenuPriority1)]
        static void testCreate()
        {
            var allAssetPaths = AssetDatabase.GetAllAssetPaths();
            var fileName = "MKRenderPipelineResources.asset";
            for(int i = 0; i < allAssetPaths.Length; ++i)
            {
                if (allAssetPaths[i].EndsWith(fileName))
                    Debug.Log(allAssetPaths[i]);
            }
        }
        
    }
}