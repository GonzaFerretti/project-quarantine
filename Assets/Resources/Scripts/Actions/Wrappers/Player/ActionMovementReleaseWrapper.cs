using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/MovementRelease")]
public class ActionMovementReleaseWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionMovementRelease();
    }
}
