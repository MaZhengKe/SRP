using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using KuanMi.Rendering.MKRP;
using UnityEditor;

using static KuanMi.Rendering.MKRP.MKMaterialProperties;
using BlendMode = KuanMi.Rendering.MKRP.BlendMode;

namespace KuanMiEditor.Rendering.MKRP
{
    internal class SurfaceOptionUIBlock : MaterialUIBlock
    {
        [Flags]
        public enum Features
        {
            None                        = 0,
            Surface                     = 1 << 0,
            BlendMode                   = 1 << 1,
            DoubleSided                 = 1 << 2,
            AlphaCutoff                 = 1 << 3,
            AlphaCutoffThreshold        = 1 << 4,
            AlphaCutoffShadowThreshold  = 1 << 5,
            DoubleSidedNormalMode       = 1 << 6,
            BackThenFrontRendering      = 1 << 7,
            ReceiveSSR                  = 1 << 8,
            ReceiveDecal                = 1 << 9,
            ShowAfterPostProcessPass    = 1 << 10,
            AlphaToMask                 = 1 << 11,
            ShowPrePassAndPostPass      = 1 << 12,
            ShowDepthOffsetOnly         = 1 << 13,
            PreserveSpecularLighting    = 1 << 14,
            Unlit                       = Surface | BlendMode | DoubleSided | AlphaCutoff | AlphaCutoffThreshold | AlphaCutoffShadowThreshold| AlphaToMask | BackThenFrontRendering | ShowAfterPostProcessPass | ShowPrePassAndPostPass | ShowDepthOffsetOnly,
            Lit                         = All ^ SurfaceOptionUIBlock.Features.ShowAfterPostProcessPass ^ ShowDepthOffsetOnly, // Lit can't be display in after postprocess pass
            All                         = ~0,
        }
        
        internal static class Styles
        {
            public const string optionText = "Surface Options";
            public const string surfaceTypeText = "Surface Type";
            public const string renderingPassText = "Rendering Pass";
            public const string blendModeText = "Blending Mode";
            public const string notSupportedInMultiEdition = "Multiple Different Values";

            public static readonly string[] surfaceTypeNames = Enum.GetNames(typeof(SurfaceType));
            public static readonly string[] blendModeNames = Enum.GetNames(typeof(BlendMode));
            public static readonly int[] blendModeValues = Enum.GetValues(typeof(BlendMode)) as int[];

            public static GUIContent transparentPrepassText = new GUIContent("Appear in Refraction", "When enabled, HDRP handles objects with this Material before the refraction pass.");

            public static GUIContent doubleSidedEnableText = new GUIContent("Double-Sided", "When enabled, HDRP renders both faces of the polygons that make up meshes using this Material. Disables backface culling.");

            public static GUIContent useShadowThresholdText = new GUIContent("Use Shadow Threshold", "Enable separate threshold for shadow pass");
            public static GUIContent alphaCutoffEnableText = new GUIContent("Alpha Clipping", "When enabled, HDRP processes Alpha Clipping for this Material.");
            public static GUIContent alphaCutoffText = new GUIContent("Threshold", "Controls the threshold for the Alpha Clipping effect.");
            public static GUIContent alphaCutoffShadowText = new GUIContent("Shadow Threshold", "Controls the threshold for shadow pass alpha clipping.");
            public static GUIContent alphaCutoffPrepassText = new GUIContent("Prepass Threshold", "Controls the threshold for transparent depth prepass alpha clipping.");
            public static GUIContent alphaCutoffPostpassText = new GUIContent("Postpass Threshold", "Controls the threshold for transparent depth postpass alpha clipping.");
            public static GUIContent alphaToMaskText = new GUIContent("Alpha To Mask", "When enabled and using MSAA, HDRP enables alpha to coverage during the depth prepass.");
            public static GUIContent transparentDepthPostpassEnableText = new GUIContent("Transparent Depth Postpass", "When enabled, HDRP renders a depth postpass for transparent objects. This improves post-processing effects like depth of field.");
            public static GUIContent transparentDepthPrepassEnableText = new GUIContent("Transparent Depth Prepass", "When enabled, HDRP renders a depth prepass for transparent GameObjects. This improves sorting.");
            public static GUIContent transparentBackfaceEnableText = new GUIContent("Back Then Front Rendering", "When enabled, HDRP renders the back face and then the front face, in two separate draw calls, to better sort transparent meshes.");
            public static GUIContent transparentWritingMotionVecText = new GUIContent("Transparent Writes Motion Vectors", "When enabled, transparent objects write motion vectors, these replace what was previously rendered in the buffer.");

            public static GUIContent zWriteEnableText = new GUIContent("Depth Write", "When enabled, transparent objects write to the depth buffer.");
            public static GUIContent transparentZTestText = new GUIContent("Depth Test", "Set the comparison function to use during the Z Testing.");
            public static GUIContent rayTracingText = new GUIContent("Recursive Rendering (Preview)");
            public static GUIContent rayTracingTextInfo = new GUIContent("When enabled, if you enabled ray tracing in your project and a recursive rendering volume override is active, Unity uses recursive rendering to render the GameObject.");

            public static GUIContent transparentSortPriorityText = new GUIContent("Sorting Priority", "Sets the sort priority (from -100 to 100) of transparent meshes using this Material. HDRP uses this value to calculate the sorting order of all transparent meshes on screen.");
            public static GUIContent enableTransparentFogText = new GUIContent("Receive fog", "When enabled, this Material can receive fog.");
            public static GUIContent transparentCullModeText = new GUIContent("Cull Mode", "For transparent objects, change the cull mode of the object.");
            public static GUIContent enableBlendModePreserveSpecularLightingText = new GUIContent("Preserve specular lighting", "When enabled, blending only affects diffuse lighting, allowing for correct specular lighting on transparent meshes that use this Material.");

            // Lit properties
            public static GUIContent doubleSidedNormalModeText = new GUIContent("Normal Mode", "Specifies the method HDRP uses to modify the normal base.\nMirror: Mirrors the normals with the vertex normal plane.\nFlip: Flips the normal.");
            public static GUIContent depthOffsetEnableText = new GUIContent("Depth Offset", "When enabled, HDRP uses the Height Map to calculate the depth offset for this Material.");

            // Displacement mapping (POM, tessellation, per vertex)
            //public static GUIContent enablePerPixelDisplacementText = new GUIContent("Per Pixel Displacement", "");

            public static GUIContent displacementModeText = new GUIContent("Displacement Mode", "Specifies the method HDRP uses to apply height map displacement to the selected element: Vertex, pixel, or tessellated vertex.\n You must use flat surfaces for Pixel displacement.");
            public static GUIContent lockWithObjectScaleText = new GUIContent("Lock With Object Scale", "When enabled, displacement mapping takes the absolute value of the scale of the object into account.");
            public static GUIContent lockWithTilingRateText = new GUIContent("Lock With Height Map Tiling Rate", "When enabled, displacement mapping takes the absolute value of the tiling rate of the height map into account.");

            // Material ID
            public static GUIContent materialIDText = new GUIContent("Material Type", "Specifies additional feature for this Material. Customize you Material with different settings depending on which Material Type you select.");
            public static GUIContent transmissionEnableText = new GUIContent("Transmission", "When enabled HDRP processes the transmission effect for subsurface scattering. Simulates the translucency of the object.");
            public static string transparentSSSErrorMessage = "Transparent Materials With SubSurface Scattering is not supported.";

            // Per pixel displacement
            public static GUIContent ppdMinSamplesText = new GUIContent("Minimum Steps", "Controls the minimum number of steps HDRP uses for per pixel displacement mapping.");
            public static GUIContent ppdMaxSamplesText = new GUIContent("Maximum Steps", "Controls the maximum number of steps HDRP uses for per pixel displacement mapping.");
            public static GUIContent ppdLodThresholdText = new GUIContent("Fading Mip Level Start", "Controls the Height Map mip level where the parallax occlusion mapping effect begins to disappear.");
            public static GUIContent ppdPrimitiveLength = new GUIContent("Primitive Length", "Sets the length of the primitive (with the scale of 1) to which HDRP applies per-pixel displacement mapping. For example, the standard quad is 1 x 1 meter, while the standard plane is 10 x 10 meters.");
            public static GUIContent ppdPrimitiveWidth = new GUIContent("Primitive Width", "Sets the width of the primitive (with the scale of 1) to which HDRP applies per-pixel displacement mapping. For example, the standard quad is 1 x 1 meter, while the standard plane is 10 x 10 meters.");

            public static GUIContent supportDecalsText = new GUIContent("Receive Decals", "Enable to allow Materials to receive decals.");

            public static GUIContent enableGeometricSpecularAAText = new GUIContent("Geometric Specular AA", "When enabled, HDRP reduces specular aliasing on high density meshes (particularly useful when the not using a normal map).");
            public static GUIContent specularAAScreenSpaceVarianceText = new GUIContent("Screen space variance", "Controls the strength of the Specular AA reduction. Higher values give a more blurry result and less aliasing.");
            public static GUIContent specularAAThresholdText = new GUIContent("Threshold", "Controls the effect of Specular AA reduction. A values of 0 does not apply reduction, higher values allow higher reduction.");

            // SSR
            public static GUIContent receivesSSRText = new GUIContent("Receive SSR/SSGI", "When enabled, this Material can receive screen space reflections and screen space global illumination.");
            public static GUIContent receivesSSRTransparentText = new GUIContent("Receive SSR Transparent", "When enabled, this Material can receive screen space reflections.");

            public static GUIContent opaqueCullModeText = new GUIContent("Cull Mode", "For opaque objects, change the cull mode of the object.");

            public static string afterPostProcessZTestInfoBox = "After post-process material wont be ZTested. Enable the \"ZTest For After PostProcess\" checkbox in the Frame Settings to force the depth-test if the TAA is disabled.";
        }
        
        // Properties common to Unlit and Lit
        MaterialProperty surfaceType = null;

        MaterialProperty alphaCutoffEnable = null;
        const string kAlphaCutoffEnabled = "_AlphaCutoffEnable";
        MaterialProperty useShadowThreshold = null;
        const string kUseShadowThreshold = "_UseShadowThreshold";
        MaterialProperty alphaCutoff = null;
        const string kAlphaCutoff = "_AlphaCutoff";
        MaterialProperty alphaCutoffShadow = null;
        const string kAlphaCutoffShadow = "_AlphaCutoffShadow";
        MaterialProperty alphaCutoffPrepass = null;
        const string kAlphaCutoffPrepass = "_AlphaCutoffPrepass";
        MaterialProperty alphaCutoffPostpass = null;
        const string kAlphaCutoffPostpass = "_AlphaCutoffPostpass";
        MaterialProperty alphaToMask = null;
        const string kAlphaToMask = kAlphaToMaskInspector;
        MaterialProperty transparentDepthPrepassEnable = null;
        const string kTransparentDepthPrepassEnable = "_TransparentDepthPrepassEnable";
        MaterialProperty transparentDepthPostpassEnable = null;
        const string kTransparentDepthPostpassEnable = "_TransparentDepthPostpassEnable";
        MaterialProperty transparentBackfaceEnable = null;
        const string kTransparentBackfaceEnable = "_TransparentBackfaceEnable";
        MaterialProperty transparentSortPriority = null;
        const string kTransparentSortPriority = MKMaterialProperties.kTransparentSortPriority;
        MaterialProperty transparentWritingMotionVec = null;
        MaterialProperty doubleSidedEnable = null;
        const string kDoubleSidedEnable = "_DoubleSidedEnable";
        MaterialProperty blendMode = null;
        const string kBlendMode = "_BlendMode";
        MaterialProperty enableBlendModePreserveSpecularLighting = null;
        MaterialProperty enableFogOnTransparent = null;
        const string kEnableFogOnTransparent = "_EnableFogOnTransparent";
        
        
        protected MaterialProperty refractionModel = null;
        MaterialProperty stencilRef = null;
        
        int renderQueue
        {
            get => (materialEditor.targets[0] as Material).renderQueue;
            set
            {
                foreach (var target in materialEditor.targets)
                {
                    (target as Material).renderQueue = value;
                }
            }
        }
        
        bool renderQueueHasMultipleDifferentValue
        {
            get
            {
                if (materialEditor.targets.Length < 2)
                    return false;

                int firstRenderQueue = renderQueue;
                for (int index = 1; index < materialEditor.targets.Length; ++index)
                {
                    if ((materialEditor.targets[index] as Material).renderQueue != firstRenderQueue)
                        return true;
                }
                return false;
            }
        }
        
        

        
        Expandable  m_ExpandableBit;
        Features    m_Features;
        int         m_LayerCount;
        public SurfaceOptionUIBlock(MaterialUIBlock.Expandable expandableBit, int layerCount = 1, Features features = Features.All)
        {
            m_ExpandableBit = expandableBit;
            m_Features = features;
            m_LayerCount = layerCount;
        }

        public override void LoadMaterialProperties()
        {
            surfaceType = FindProperty(kSurfaceType);
        }

        public override void OnGUI()
        {
            using (var header = new MaterialHeaderScope(Styles.optionText, (uint)m_ExpandableBit, materialEditor))
            {
                if (header.expanded)
                    DrawSurfaceOptionGUI();
            }
        }
        
        void DrawSurfaceOptionGUI()
        {
            if ((m_Features & Features.Surface) != 0)
                DrawSurfaceGUI();

            // if ((m_Features & Features.AlphaCutoff) != 0)
            //     DrawAlphaCutoffGUI();
            //
            // if ((m_Features & Features.DoubleSided) != 0)
            //     DrawDoubleSidedGUI();
            //
            // DrawLitSurfaceOptions();
        }

        void DrawSurfaceGUI()
        {
            SurfaceTypePopup();
        }
        
        
        void SurfaceTypePopup()
        {
            if (surfaceType == null)
                return;

            // TODO: does not work with multi-selection
            Material material = materialEditor.target as Material;

            var mode = (SurfaceType)surfaceType.floatValue;
            var renderQueueType = MKRenderQueue.GetTypeByRenderQueueValue(material.renderQueue);
            bool alphaTest = material.HasProperty(kAlphaCutoffEnabled) && material.GetFloat(kAlphaCutoffEnabled) > 0.0f;
            bool receiveDecal = material.HasProperty(kSupportDecals) && material.GetFloat(kSupportDecals) > 0.0f;

            // Shader graph only property, used to transfer the render queue from the shader graph to the material,
            // because we can't use the renderqueue from the shader as we have to keep the renderqueue on the material side.
            if (material.HasProperty("_RenderQueueType"))
            {
                renderQueueType = (MKRenderQueue.RenderQueueType)material.GetFloat("_RenderQueueType");
            }
            // To know if we need to update the renderqueue, mainly happens if a material is created from a shader graph shader
            // with default render-states.
            bool renderQueueTypeMismatchRenderQueue = MKRenderQueue.GetTypeByRenderQueueValue(material.renderQueue) != renderQueueType;

            EditorGUI.showMixedValue = surfaceType.hasMixedValue;
            var newMode = (SurfaceType)EditorGUILayout.Popup(Styles.surfaceTypeText, (int)mode, Styles.surfaceTypeNames);
            if (newMode != mode) //EditorGUI.EndChangeCheck is called even if value remain the same after the popup. Prefer not to use it here
            {
                materialEditor.RegisterPropertyChangeUndo(Styles.surfaceTypeText);
                surfaceType.floatValue = (float)newMode;
            }
            EditorGUI.showMixedValue = false;

            bool isMixedRenderQueue = surfaceType.hasMixedValue || renderQueueHasMultipleDifferentValue;
            bool showAfterPostProcessPass = (m_Features & Features.ShowAfterPostProcessPass) != 0;
            bool showPreRefractionPass = refractionModel == null || refractionModel.floatValue == 0;
            bool showLowResolutionPass = true;

            EditorGUI.showMixedValue = isMixedRenderQueue;
            ++EditorGUI.indentLevel;


            --EditorGUI.indentLevel;
            EditorGUI.showMixedValue = false;

            if (material.HasProperty("_RenderQueueType"))
                material.SetFloat("_RenderQueueType", (float)renderQueueType);
        }

    }
}