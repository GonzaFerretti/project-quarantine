using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHide : BaseActionInteractableObject
{
    public override void Do(InteractableObject obj)
    {
        obj.animator.SetBool("open", true);
    }
}
