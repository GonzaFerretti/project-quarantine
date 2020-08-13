using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Hide")]
public class ActionHideWrapper : ActionBaseInteractWrapper
{
    public int redirection;
    public int goalLocation;
    public Item dragUI;

    public override void SetAction()
    {
        action = new ActionHide(goalLocation , redirection, interactionDistance, dragUI);
        base.SetAction();
    }
}
