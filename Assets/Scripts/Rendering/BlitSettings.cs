using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class BlitSettings
{
    public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

    public Material blitMaterial = null;
    public int blitMaterialPassIndex = 0;
    public bool setInverseViewMatrix = false;

    public Target srcType = Target.CameraColor;
    public string srcTextureId = "_CameraColorTexture";
    public RenderTexture srcTextureObject;

    public Target dstType = Target.CameraColor;
    public string dstTextureId = "_BlitPassTexture";
    public RenderTexture dstTextureObject;
}

public enum Target
{
    CameraColor,
    TextureID,
    RenderTextureObject
}