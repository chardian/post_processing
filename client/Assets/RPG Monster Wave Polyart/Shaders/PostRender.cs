using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRender : MonoBehaviour
{
    public Shader postShader;
    public RenderTexture rt;
    private Material _mat;

    private Material Mat
    {
        get
        {
            if (_mat == null)
            {
                _mat = new Material(postShader);
                _mat.hideFlags = HideFlags.HideAndDontSave;
            }

            return _mat;
        }
    }

    private RenderTexture temp1;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (Mat != null)
        {
            Mat.SetTexture("_SplitTex", rt);
            temp1 = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
            Graphics.Blit(src, temp1, Mat, 0);
            Graphics.Blit(temp1, dest);
            temp1.Release();
        }
    }
}