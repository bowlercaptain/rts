using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class ZoomRenderPipeline : RenderPipeline
{
    // Use this variable to a reference to the Render Pipeline Asset that was passed to the constructor
    private ZoomRenderPipelineAsset renderPipelineAsset;

    // The constructor has an instance of the ExampleRenderPipelineAsset class as its parameter.
    public ZoomRenderPipeline(ZoomRenderPipelineAsset asset)
    {
        renderPipelineAsset = asset;
    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
       
    }
}
