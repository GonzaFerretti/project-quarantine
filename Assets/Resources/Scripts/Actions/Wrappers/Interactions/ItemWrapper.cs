using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : InteractableObject
{
    public Item item;
    public Material standardMaterial;
    public Material outlineMaterial;
    public Material seeThroughMat;
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
        InitShader();
    }

    void InitShader()
    {
        foreach(MeshRenderer meshren  in _meshRenderers)
        {
            Material[] usualMat = meshren.materials;
            Material[] transMat = new Material[meshren.materials.Length];
            for (int i = 0; i < meshren.materials.Length; i++)
            {
                Material mat = new Material(seeThroughMat);
                mat.SetTexture("_Albedo", usualMat[i].GetTexture("_MainTex"));
                mat.SetTexture("_Normal", usualMat[i].GetTexture("_BumpMap"));
                mat.SetFloat("_Glossiness", usualMat[i].GetFloat("_Glossiness"));
                mat.SetFloat("_Metallic", usualMat[i].GetFloat("_Metallic"));
                mat.SetColor("_Color", item.seeThroughColor);
                transMat[i] = mat;
            }
            meshren.materials = transMat;
        }
    }
        /*
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
        }*/
    }
