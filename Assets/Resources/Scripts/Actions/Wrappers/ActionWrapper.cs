using UnityEngine;

public abstract class ActionWrapper : ScriptableObject
{
    public IAction action;
    public ActionKeyLinks actionKey;
    public abstract void SetAction();
}
