using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Drag Body")]
public class ActionStartDragBodyWrapper : ActionBaseInteractWrapper
{
    public Item dragUI;
    public override void SetAction()
    {
        action = new ActionStartDragBody(interactionDistance, dragUI);
        base.SetAction();
    }
}
