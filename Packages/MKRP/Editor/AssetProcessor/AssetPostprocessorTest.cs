using System;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    public class AssetPostprocessorTest: AssetPostprocessor
    {
        public void OnPreprocessMaterialDescription(MaterialDescription description, Material material,
            AnimationClip[] animations)
        {
            Debug.Log("OnPreprocessMaterialDescription");
            Debug.Log(material);
        }
    }
}