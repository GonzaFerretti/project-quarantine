using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : InteractableObject
{
    public Item item;
    public Material standardMaterial;
    public Material outlineMaterial;
    Renderer[] _meshRenderers;

    protected override void Start()
    {
        //tentative;
        base.Start();
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        try
        {

            GetComponent<ParticleSystem>().Play();
        }
        catch
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    void SetMeshOutLine (bool isOutlined)
    {
        if (!isOutlined)
        {
            foreach (MeshRenderer rend in _meshRenderers)
            {
               rend.material = standardMaterial;
            }
        }
        else
        {
            foreach (MeshRenderer rend in _meshRenderers)
            {
                rend.material = outlineMaterial;
            }
        }
    }

    public void ActivateOutline()
    {
        outlineMaterial.SetTexture("_texture", standardMaterial.GetTexture("_MainTex"));
        outlineMaterial.SetTexture("_normal", standardMaterial.GetTexture("_BumpMap"));
        SetMeshOutLine(true);
    }

    public void DisableOutline()
    {
        SetMeshOutLine(false);
    }
}
