using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : InteractableObject
{
    public Item item;
    public void Start()
    {
        if (item._mesh)
        {
            GetComponent<MeshFilter>().mesh = item._mesh;
        }
    }
}
