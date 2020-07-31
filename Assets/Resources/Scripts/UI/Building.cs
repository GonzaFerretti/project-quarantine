using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    enum HideStates
    {
        hidden,
        waitingToUnhide,
        interruptingUnhiding,
        visible,
    }


    Material[] transMat;
    Material[] usualMat;
    public Material transBaseMat;
    Animator anim;
    MeshRenderer meshren;
    HideStates currentHideState = HideStates.visible;

    [Range(0, 1)]
    public float opacity;

    public void Start()
    {
        anim = GetComponent<Animator>();
        meshren = GetComponent<MeshRenderer>();
        usualMat = meshren.materials;
        transMat = new Material[meshren.materials.Length];
        for (int i = 0; i < meshren.materials.Length; i++)
        {
            Material mat = new Material(transBaseMat);
            mat.SetTexture("_albedo", usualMat[i].GetTexture("_MainTex"));
            mat.SetTexture("_normal", usualMat[i].GetTexture("_BumpMap"));
            mat.SetTexture("_metalicSmoothness", usualMat[i].GetTexture("_MetallicGlossMap"));
            mat.SetTexture("_ao", usualMat[i].GetTexture("_OcclusionMap"));
            transMat[i] = mat;
        }
        meshren.materials = transMat;
    }

    public void Hide(float time)
    {
        if (currentHideState == HideStates.visible)
        {
            anim.SetBool("isHiding", true);
            StartCoroutine(StartTimerToUnHideAgain(time));
            currentHideState = HideStates.waitingToUnhide;
        }
        else if (currentHideState == HideStates.waitingToUnhide)
        {
            currentHideState = HideStates.interruptingUnhiding;
        }
    }

    IEnumerator StartTimerToUnHideAgain(float time)
    {
        float finalTime = Time.time + time;
        while (Time.time < finalTime)
        {
            yield return new WaitForEndOfFrame();
            if (currentHideState == HideStates.interruptingUnhiding)
            {
                currentHideState = HideStates.waitingToUnhide;
                StartCoroutine(StartTimerToUnHideAgain(time));
                yield break;
            }
        }
        currentHideState = HideStates.visible;
        anim.SetBool("isHiding", false);
    }

    public void Update()
    {
        UpdateOpacity();
    }

    public void UpdateOpacity()
    {
        foreach (Material mat in transMat)
        {
            mat.SetFloat("_opacity", opacity);
        }
    }
}
