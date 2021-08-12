using UnityEngine;
using UnityEngine.Rendering;

namespace SRP
{
    public class MKRenderPipline : RenderPipeline
    {
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var camera in cameras)
            {               
                //设置渲染相关相机参数,包含相机的各个矩阵和剪裁平面等
                context.SetupCameraProperties(camera);     
                //绘制天空球
                context.DrawSkybox(camera);
                //开始执行上下文
                context.Submit();
            }
        }
    }
}
