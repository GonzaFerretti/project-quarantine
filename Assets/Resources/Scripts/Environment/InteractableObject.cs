using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ActionWrapper requiredAction;
    public InteractableFeedbackWrapper feedback;

    public void Interact(ModelPlayable c)
    {
        if (feedback.action == null) feedback.SetAction();
        feedback.action.Do(this);
    }
}
