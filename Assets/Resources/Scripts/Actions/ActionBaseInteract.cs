using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBaseInteract : IAction
{
    public abstract void Do(Model m);
    public float interactionDistance;
}
