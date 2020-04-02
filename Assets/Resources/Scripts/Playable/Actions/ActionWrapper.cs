using UnityEngine;

public abstract class ActionWrapper : ScriptableObject
{
    public IAction action;
    public abstract void SetAction();
}
