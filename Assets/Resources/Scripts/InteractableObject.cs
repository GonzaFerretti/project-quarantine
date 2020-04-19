using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ActionWrapper requiredAction;
    public InteractableFeedbackWrapper feedback;

    public void Interact(ModelPlayable c)
    {
        for (int i = 0; i < c.availableActions.Count; i++)
        {
            if(c.availableActions[i] == requiredAction)
            {
                c.availableActions[i].action.Do(c);

                if (feedback.action == null) feedback.SetAction();
                feedback.action.Do(this);
                break;
            }
        }
    }
}
