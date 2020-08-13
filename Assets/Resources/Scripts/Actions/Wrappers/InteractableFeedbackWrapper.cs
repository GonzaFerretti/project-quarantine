using UnityEngine;

public abstract class InteractableFeedbackWrapper : ScriptableObject
{
    public BaseActionInteractableObject action;
    public SoundClip clip;
    public virtual void SetAction()
    {
        action.clip = clip;
    }
}
