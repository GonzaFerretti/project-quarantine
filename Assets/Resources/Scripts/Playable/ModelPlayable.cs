using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ModelPlayable : ModelChar
{
    public Inventory inv;
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < availableActions.Count; i++)
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
    
}
