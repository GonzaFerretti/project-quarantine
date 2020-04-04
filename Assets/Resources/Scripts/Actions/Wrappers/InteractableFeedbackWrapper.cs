using UnityEngine;

public abstract class InteractableFeedbackWrapper : ScriptableObject
{
    public IActionInteractableObject action;
    public abstract void SetAction();
}
