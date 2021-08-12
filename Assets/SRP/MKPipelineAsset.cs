using UnityEngine;
using UnityEngine.Rendering;

namespace SRP
{
    [CreateAssetMenu(menuName = "Rendering/MKPipeline")]
    public class MKPipelineAsset : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new MKRenderPipline();
        }
    }
}