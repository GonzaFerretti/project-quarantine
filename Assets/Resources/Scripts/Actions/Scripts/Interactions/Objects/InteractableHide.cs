using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHide : IActionInteractableObject
{
    public void Do(InteractableObject obj)
    {
        //Animation
        Debug.Log("Hide");
    }
}
