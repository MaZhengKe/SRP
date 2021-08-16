using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KuanMiEditor.Rendering.MKRP
{
    class MaterialUIBlockList: List<MaterialUIBlock>
    {
        
        [System.NonSerialized]
        bool                        m_Initialized = false;

        Material[]                  m_Materials;
        
        /// <summary>
        /// Parent of the ui block list, in case of nesting (Layered Lit material)
        /// </summary>
        public MaterialUIBlockList  parent;
        
        
        /// <summary>
        /// List of materials currently selected in the inspector
        /// </summary>
        public Material[] materials => m_Materials;
        
        
        public MaterialUIBlockList(MaterialUIBlockList parent) => this.parent = parent;
        
        
        /// <summary>
        /// Construct a ui block list
        /// </summary>
        /// <returns></returns>
        public MaterialUIBlockList() : this(null) {}
        
        
        
        /// <summary>
        /// Render the list of ui blocks added contained in the materials property
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="properties"></param>
        public void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            // Use default labelWidth
            EditorGUIUtility.labelWidth = 0f;
            Initialize(materialEditor, properties);
            foreach (var uiBlock in this)
            {
                try
                {
                    // We load material properties at each frame because materials can be animated and to make undo/redo works
                    uiBlock.UpdateMaterialProperties(properties);
                    uiBlock.OnGUI();
                }
                // Never catch ExitGUIException as they are used to handle color picker and object pickers.
                catch (ExitGUIException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
        
        
        
        
        /// <summary>
        /// Initialize the ui blocks, can be called at every frame, a guard is prevents more that one initialization
        /// <remarks>This function is called automatically by MaterialUIBlockList.OnGUI so you only need this when you want to render the UI Blocks in a custom order</remarks>
        /// </summary>
        public void Initialize(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            if (m_Initialized) return;
            foreach (var uiBlock in this)
                uiBlock.Initialize(materialEditor, properties, this);

            m_Materials = materialEditor.targets.Select(target => target as Material).ToArray();
            m_Initialized = true;
        }
    }
    
    
}