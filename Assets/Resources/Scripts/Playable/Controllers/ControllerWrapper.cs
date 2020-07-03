using System.Collections;
using UnityEngine;

public abstract class ControllerWrapper : ScriptableObject
{
    public IController myController;
    public abstract void SetController();
    public abstract ControllerWrapper Clone();
}