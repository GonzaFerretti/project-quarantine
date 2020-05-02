using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : InteractableObject
{
    public Item item;
    protected override void Start()
    {
        //tentative;
        base.Start();
        if (item.mesh)
        {
            GetComponent<MeshFilter>().mesh = item.mesh;
        }
    }
}
