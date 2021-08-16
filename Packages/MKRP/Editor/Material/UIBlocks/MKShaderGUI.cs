using KuanMi.Rendering.MKRP;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace KuanMiEditor.Rendering.MKRP
{
    internal abstract class MKShaderGUI : ShaderGUI
    {
        internal protected bool m_FirstFrame = true;
        
        protected abstract void SetupMaterialKeywordsAndPassInternal(Material material);

        protected void ApplyKeywordsAndPassesIfNeeded(bool changed, Material[] materials)
        {
            // !!! HACK !!!
            // When a user creates a new Material from the contextual menu, the material is created from the editor code and the appropriate shader is applied to it.
            // This means that we never setup keywords and passes for a newly created material. The material is then in an invalid state.
            // To work around this, as the material is automatically selected when created, we force an update of the keyword at the first "frame" of the editor.

            // Apply material keywords and pass:
            if (changed || m_FirstFrame)
            {
                m_FirstFrame = false;

                foreach (var material in materials)
                    SetupMaterialKeywordsAndPassInternal(material);
            }
        }

        /// <summary>
        /// Unity calls this function when it displays the GUI. This method is sealed so you cannot override it. To implement your custom GUI, use OnMaterialGUI instead.
        /// </summary>
        public sealed override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            if (!(RenderPipelineManager.currentPipeline is MKRenderPipeline))
            {
                EditorGUILayout.HelpBox(
                    "Editing MKRP materials is only supported when an MKRP asset assigned in the graphic settings",
                    MessageType.Warning);
            }
            else
            {
                OnMaterialGUI(materialEditor, props);
            }
        }

        /// <summary>
        /// Implement your custom GUI in this function. To display a UI similar to HDRP shaders, use a MaterialUIBlock.
        /// </summary>
        /// <param name="materialEditor">The current material editor.</param>
        /// <param name="props">The list of properties the material has.</param>
        protected abstract void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] props);
    }
}