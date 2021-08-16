using KuanMi.Rendering.MKRP;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEditor.Rendering;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    using CED = CoreEditorDrawer<SerializedMKRenderPipelineAsset>;
    static partial class MKRenderPipelineUI
    {
        enum Expandable
        {
            CameraFrameSettings = 1 << 0,
            BakedOrCustomProbeFrameSettings = 1 << 1,
            RealtimeProbeFrameSettings = 1 << 2,
            General = 1 << 3,
            Rendering = 1 << 4,
            Lighting = 1 << 5,
            Material = 1 << 6,
            LightLoop = 1 << 7,
            Cookie = 1 << 8,
            Reflection = 1 << 9,
            Sky = 1 << 10,
            Shadow = 1 << 11,
            Decal = 1 << 12,
            PostProcess = 1 << 13,
            DynamicResolution = 1 << 14,
            LowResTransparency = 1 << 15,
            PostProcessQuality = 1 << 16,
            DepthOfFieldQuality = 1 << 17,
            MotionBlurQuality = 1 << 18,
            BloomQuality = 1 << 19,
            ChromaticAberrationQuality = 1 << 20,
            XR = 1 << 21,
            LightLayer = 1 << 22,
            SSAOQuality = 1 << 23,
            ContactShadowQuality = 1 << 24,
            LightingQuality = 1 << 25,
            SSRQuality = 1 << 26,
            VirtualTexturing = 1 << 27,
            FogQuality = 1 << 28
        }
        static readonly ExpandedState<Expandable, MKRenderPipelineAsset> k_ExpandedState = new ExpandedState<Expandable, MKRenderPipelineAsset>(Expandable.CameraFrameSettings | Expandable.General, "MKRP");

        static void Drawer_SectionRenderingUnsorted(SerializedMKRenderPipelineAsset serialized, Editor owner)
        {
            EditorGUILayout.PropertyField(serialized.renderPipelineSettings.colorBufferFormat, Styles.colorBufferFormatContent);
        }


        public static readonly CED.IDrawer Inspector;

        static MKRenderPipelineUI()
        {
            Inspector = CED.Group(
                CED.FoldoutGroup(Styles.renderingSectionTitle,Expandable.Rendering, k_ExpandedState,
                    CED.Group(GroupOption.Indent, Drawer_SectionRenderingUnsorted))
                );
            
        }
    }
}