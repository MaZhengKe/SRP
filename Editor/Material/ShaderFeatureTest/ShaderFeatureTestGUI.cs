using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace KuanMiEditor.Rendering.MKRP
{
    class ShaderFeatureTestGUI : ShaderGUI
    {
        private MaterialUIBlockList uiBlocks = new MaterialUIBlockList();
        private static readonly int ShowTex = Shader.PropertyToID("_ShowTex");
        private static readonly int IsRed = Shader.PropertyToID("_IsRed");

        protected void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            using var changed = new EditorGUI.ChangeCheckScope();
            
            //ApplyKeywordsAndPassesIfNeeded(changed.changed, uiBlocks.materials);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);
            var materials = materialEditor.targets.Select(target => target as Material).ToArray();
            Debug.Log(materials.Length);
            foreach (var material in materials)
            {
                Debug.Log(material);
                Debug.Log(material.GetInt(ShowTex) != 0);
                CoreUtils.SetKeyword(material, "_MKRP_SHOW_TEX", material.GetInt(ShowTex) != 0);
                CoreUtils.SetKeyword(material, "_RED", material.GetInt(IsRed) != 0);
                
            }
        }
    }
}