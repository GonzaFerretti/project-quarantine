using UnityEngine;

[CreateAssetMenu (menuName = "Environment/Objects/Hide")]
public class InteractableHideWrapper : InteractableFeedbackWrapper
{
    public override void SetAction()
    {
        action = new InteractableHide();
    }
}
