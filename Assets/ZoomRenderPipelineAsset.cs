using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/ZoomConfig")]
public class ZoomRenderPipelineAsset : RenderPipelineAsset
{
    // This data can be defined in the Inspector for each Render Pipeline Asset
    public Color exampleColor;
    public string exampleString;

    // Unity calls this method before rendering the first frame.
    // If a setting on the Render Pipeline Asset changes, Unity destroys the current Render Pipeline Instance and calls this method again before rendering the next frame.
    protected override RenderPipeline CreatePipeline()
    {
        // Instantiate the Render Pipeline that this custom SRP uses for rendering, and pass a reference to this Render Pipeline Asset.
        // The Render Pipeline Instance can then access the configuration data defined above.
        return new ZoomRenderPipeline(this);
    }
}
