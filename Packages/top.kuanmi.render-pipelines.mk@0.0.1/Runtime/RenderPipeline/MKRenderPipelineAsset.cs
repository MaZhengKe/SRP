using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace KuanMi.Rendering.MKRP
{
    public partial class MKRenderPipelineAsset : RenderPipelineAsset
    {
        MKRenderPipelineAsset()
        {
            
        }
        
        void Reset() => OnValidate();
        
        [SerializeField]
        RenderPipelineSettings m_RenderPipelineSettings = RenderPipelineSettings.NewDefault();
        
        public RenderPipelineSettings currentPlatformRenderPipelineSettings => m_RenderPipelineSettings;

        [SerializeField]
        internal bool allowShaderVariantStripping = true;
        
        [SerializeField]
        RenderPipelineResources m_RenderPipelineResources;

        internal RenderPipelineResources renderPipelineResources
        {
            get => m_RenderPipelineResources;
            set => m_RenderPipelineResources = value;
        }
        public override Shader defaultShader => m_RenderPipelineResources?.shaders.defaultPS;

        protected override RenderPipeline CreatePipeline()
            => new MKRenderPipeline(this);

        protected override void OnValidate()
        {
            Debug.Log("OnValidate");
            if (GraphicsSettings.currentRenderPipeline == this){
                Debug.Log("currentRenderPipeline == this");
                base.OnValidate();
            }
        }
    }
}