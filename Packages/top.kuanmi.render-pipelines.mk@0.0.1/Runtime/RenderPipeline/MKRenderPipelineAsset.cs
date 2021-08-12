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
        
        protected override RenderPipeline CreatePipeline()
            => new MKRenderPipeline(this);

        protected override void OnValidate()
        {
            Debug.Log("OnValidate");
            if (GraphicsSettings.currentRenderPipeline == this)
                base.OnValidate();
        }
    }
}