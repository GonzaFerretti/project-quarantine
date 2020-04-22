using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelPlayable : ModelChar
{
    float _sneakSpeed;
    public Inventory inv;
    public CharacterAttributes myAttributes;

    protected override void Start()
    {
        base.Start();
        SetAttributes(myAttributes);
        for (int i = 0; i < availableActions.Count; i++)
        {
            availableActions[i].SetAction();
        }
        if (controller is PlayerController)
        {
            (controller as PlayerController).StartFunction();
        }

        inv = inv.cloneInvTemplate();
        inv.initializeInventory(this);

        //for (int i = 0; i < (controller as PlayerController).passiveActions.Length; i++)
        //{
        //    if ((controller as PlayerController).passiveActions[i].action == null) (controller as PlayerController).passiveActions[i].SetAction();
        //}
    }

    void SetAttributes(CharacterAttributes attributes)
    {
        walkSpeed = attributes.walkSpeed;
        runSpeed = attributes.runSpeed;
        _sneakSpeed = attributes.sneakSpeed;

        currentSpeed = walkSpeed;

        availableActions = new List<ActionWrapper>();
        for (int i = 0; i < attributes.innateActions.Length; i++)
        {
            availableActions.Add(attributes.innateActions[i]);
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
