using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

// Include material common properties names
using static KuanMi.Rendering.MKRP.MKMaterialProperties;

namespace KuanMiEditor.Rendering.MKRP
{
    class UnlitGUI : MKShaderGUI
    {
        MaterialUIBlockList uiBlocks = new MaterialUIBlockList
        {
            new SurfaceOptionUIBlock(MaterialUIBlock.Expandable.Base, features: SurfaceOptionUIBlock.Features.Unlit),
            new UnlitSurfaceInputsUIBlock(MaterialUIBlock.Expandable.Input),
            new TransparencyUIBlock(MaterialUIBlock.Expandable.Transparency),
            new EmissionUIBlock(MaterialUIBlock.Expandable.Emissive),
            new AdvancedOptionsUIBlock(MaterialUIBlock.Expandable.Advance,
                AdvancedOptionsUIBlock.Features.Instancing | AdvancedOptionsUIBlock.Features.AddPrecomputedVelocity)
        };

        protected override void OnMaterialGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            using var changed = new EditorGUI.ChangeCheckScope();
            uiBlocks.OnGUI(materialEditor, props);
            ApplyKeywordsAndPassesIfNeeded(changed.changed, uiBlocks.materials);
        }

        protected override void SetupMaterialKeywordsAndPassInternal(Material material) =>
            SetupUnlitMaterialKeywordsAndPass(material);

        public static void SetupUnlitMaterialKeywordsAndPass(Material material)
        {
            material.SetupBaseUnlitKeywords();
            material.SetupBaseUnlitPass();

            if (material.HasProperty(kEmissiveColorMap))
                CoreUtils.SetKeyword(material, "_EMISSIVE_COLOR_MAP", material.GetTexture(kEmissiveColorMap));

            // All the bits exclusively related to lit are ignored inside the BaseLitGUI function.
            //BaseLitGUI.SetupStencil(material, receivesSSR: false, useSplitLighting: false);

            if (material.HasProperty(kAddPrecomputedVelocity))
            {
                CoreUtils.SetKeyword(material, "_ADD_PRECOMPUTED_VELOCITY",
                    material.GetInt(kAddPrecomputedVelocity) != 0);
            }
        }
    }
}