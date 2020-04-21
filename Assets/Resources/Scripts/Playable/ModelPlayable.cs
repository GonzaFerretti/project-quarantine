﻿using UnityEngine;
﻿using System.Collections;
using System.Collections.Generic;

public class ModelPlayable : ModelChar
{
    float _sneakSpeed;
    public Inventory inv;
    public ActionWrapper[] availableActions;
    public CharacterAttributes myAttributes;

    protected override void Start()
    {
        base.Start();
        SetAttributes(myAttributes);
        for (int i = 0; i < availableActions.Length; i++)
        {
            availableActions[i].SetAction();
        }
        if (controller is PlayerController)
        {
            (controller as PlayerController).startFunction();
        }

        inv = inv.cloneInvTemplate();
        inv.initializeInventory(this);
    }

    void SetAttributes(CharacterAttributes attributes)
    {
        walkSpeed = attributes.walkSpeed;
        runSpeed = attributes.runSpeed;
        _sneakSpeed = attributes.sneakSpeed;

        currentSpeed = walkSpeed;

        availableActions = new ActionWrapper[attributes.innateActions.Length];
        for (int i = 0; i < availableActions.Length; i++)
        {
            availableActions[i] = attributes.innateActions[i];
        }
        MeshFilter myMesh = GetComponent<MeshFilter>();
        myMesh.mesh = attributes.mesh;
        //MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        //meshRenderer.materials = new Material[attributes.materials.Length];
        //for (int i = 0; i < attributes.materials.Length; i++)
        //{
        //    meshRenderer.materials[i] = attributes.materials[i];
        //}
    }
}
