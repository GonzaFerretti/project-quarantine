using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Grab")]
public class ActionGrabWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionGrab();
    }
}
