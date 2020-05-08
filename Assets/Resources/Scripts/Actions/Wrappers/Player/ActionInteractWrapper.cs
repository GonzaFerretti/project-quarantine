using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Interact")]
public class ActionInteractWrapper : ActionWrapper
{
    public float interactionDistance;
    public float angleArc;
    public float angleDensity;

    public override void SetAction()
    {
        action = new ActionInteract(interactionDistance, angleArc, angleDensity);
    }
}
