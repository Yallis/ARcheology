using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactCleaner : MonoBehaviour
{
    [SerializeField] private Material clearMaterial;
    [SerializeField] private MeshRenderer artifactRenderer;

    [SerializeField] private GameObject dirtVFX;

    public void Clean()
    {
        if (artifactRenderer != null && clearMaterial != null)
        {
            artifactRenderer.material = clearMaterial;
            if (dirtVFX != null)
                SetDirtVFX(false);
        }
    }

    public void SetDirtVFX(bool dvfx)
    {
        if (dirtVFX != null)
            dirtVFX.SetActive(dvfx);
    }
}
