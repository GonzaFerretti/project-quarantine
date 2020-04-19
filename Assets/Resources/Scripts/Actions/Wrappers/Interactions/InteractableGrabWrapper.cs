using UnityEngine;

[CreateAssetMenu(menuName = "Environment/Objects/Grab")]
public class InteractableGrabWrapper : InteractableFeedbackWrapper
{
    public override void SetAction()
    {
        action = new InteractableGrab();
    }
}
