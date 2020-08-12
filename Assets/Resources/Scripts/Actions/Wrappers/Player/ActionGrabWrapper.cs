using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Grab")]
public class ActionGrabWrapper : ActionBaseInteractWrapper
{
    public override void SetAction()
    {
        action = new ActionGrab(interactionDistance);
        base.SetAction();
    }
}
