using System;
using UnityEngine;

[Serializable]
public class NodeAction
{
    public AIEnum myNextAction;
    public float rotationSpeed;
    public float secondsTillRotationEnd;
    public Vector3 targetView;
}
   public enum AIEnum { Relocate, Rotate, Guard };