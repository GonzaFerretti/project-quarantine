using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : InteractableObject
{
    public GameObject openColliders;
    public GameObject closedColliders;
    public bool canBeCut;
    protected override void Start()
    {
        base.Start();
        requiredAction.SetAction();
        if (!canBeCut)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    public void switchColliders()
    {
        closedColliders.SetActive(false);
        openColliders.SetActive(true);
    }
}
