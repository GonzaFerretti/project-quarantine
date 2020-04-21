using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGrab : IActionInteractableObject
{
    public void Do(InteractableObject obj)
    {
        Object.Destroy(obj.gameObject);
    }
}
