using System;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    internal struct MaterialHeaderScope : IDisposable
    {
        public readonly bool expanded;
        bool spaceAtEnd;
        
        public MaterialHeaderScope(string title, uint bitExpanded, MaterialEditor materialEditor, bool spaceAtEnd = true, Color colorDot = default(Color), bool subHeader = false)
        {
            bool beforeExpended = materialEditor.GetExpandedAreas(bitExpanded);

#if !UNITY_2020_1_OR_NEWER
            oldIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = subHeader ? 1 : 0; //fix for preset in 2019.3 (preset are one more indentation depth in material)
#endif

            this.spaceAtEnd = spaceAtEnd;
            if (!subHeader)
                CoreEditorUtils.DrawSplitter();
            GUILayout.BeginVertical();

            bool saveChangeState = GUI.changed;
            if (colorDot != default(Color))
                title = "   " + title;
            expanded = subHeader
                ? CoreEditorUtils.DrawSubHeaderFoldout(title, beforeExpended)
                : CoreEditorUtils.DrawHeaderFoldout(title, beforeExpended);
            if (colorDot != default(Color))
            {
                Rect dotRect = GUILayoutUtility.GetLastRect();
                dotRect.width = 5;
                dotRect.height = 5;
                dotRect.y += 7;
                dotRect.x += 17;
                EditorGUI.DrawRect(dotRect, colorDot);
            }
            if (expanded ^ beforeExpended)
            {
                materialEditor.SetExpandedAreas((uint)bitExpanded, expanded);
                saveChangeState = true;
            }
            GUI.changed = saveChangeState;

            if (expanded)
                ++EditorGUI.indentLevel;
        }
        
        public void Dispose()
        {
            if (expanded)
            {
                if (spaceAtEnd && (Event.current.type == EventType.Repaint || Event.current.type == EventType.Layout))
                    EditorGUILayout.Space();
                --EditorGUI.indentLevel;
                GUILayout.EndVertical();
            }
        }
    }
}