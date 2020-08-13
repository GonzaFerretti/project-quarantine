using UnityEngine;

public abstract class ActionMapWrapper : ScriptableObject
{
    public IActionMap myMapAction;
    public abstract void SetAction();
    public abstract ActionMapWrapper Clone();
}