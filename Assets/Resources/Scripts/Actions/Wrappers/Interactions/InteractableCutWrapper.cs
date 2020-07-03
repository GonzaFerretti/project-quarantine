using UnityEngine;

[CreateAssetMenu(menuName = "Environment/Objects/Cut")]
public class InteractableCutWrapper : InteractableFeedbackWrapper
{

    public override void SetAction()
    {
        action = new InteractableCut();
    }
}

