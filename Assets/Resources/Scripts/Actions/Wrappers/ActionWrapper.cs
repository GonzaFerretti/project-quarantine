using UnityEngine;

public class ActionWrapper : ScriptableObject
{
    public BaseAction action;
    public ActionKeyLinks actionKey;
    public SoundClip sound;
    public virtual void SetAction()
    {
        action.clip = sound;
    }
}
