using UnityEngine;

[CreateAssetMenu(menuName = "Environment/Objects/Open")]
public class InteractableOpenWrapper : InteractableFeedbackWrapper
{
    public override void SetAction()
    {
        action = new InteractableOpen();
    }
}
