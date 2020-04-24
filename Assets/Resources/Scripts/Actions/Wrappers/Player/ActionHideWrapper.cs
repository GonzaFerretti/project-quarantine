using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Hide")]
public class ActionHideWrapper : ActionWrapper
{
    public float interactionDistance;

    public override void SetAction()
    {
        action = new ActionHide(interactionDistance);
    }
}
