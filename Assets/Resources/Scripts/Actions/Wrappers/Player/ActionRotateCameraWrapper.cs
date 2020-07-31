using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Rotate Camera")]
public class ActionRotateCameraWrapper : ActionWrapper
{
    public float directionMultiplier;
    public float step;

    public override void SetAction()
    {
        action = new ActionRotateCamera(directionMultiplier,step);
    }
}