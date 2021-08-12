using UnityEditor;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    static partial class MKRenderPipelineUI
    {
        public class Styles
        {
            public static readonly GUIContent renderingSectionTitle = EditorGUIUtility.TrTextContent("Rendering");
            public static readonly GUIContent colorBufferFormatContent = EditorGUIUtility.TrTextContent("Color Buffer Format", "Specifies the format used by the scene color render target. R11G11B10 is a faster option and should have sufficient precision.");

        }
    }
}