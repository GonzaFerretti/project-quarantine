using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Interact")]
public class ActionInteractWrapper : ActionWrapper
{
    public float interactionDistance;

    public override void SetAction()
    {
        action = new ActionInteract(interactionDistance);
    }
}
