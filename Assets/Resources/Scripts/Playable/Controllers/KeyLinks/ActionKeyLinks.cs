using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action + Key Link")]
public class ActionKeyLinks : ScriptableObject
{
    public ActionWrapper myAction;
    public KeyCode key;
    public TriggerType triggerType;
    public bool CheckTrigger()
    {
        return triggerType.CheckTrigger(key);
    }
    // If changekey will be used to allow the end user to change key bindings, we shouldn't allow them to modify also the trigger type, so it was omitted in the constructor parameters.
    public void ChangeKey(KeyCode newKey)
    {
        key = newKey;
    }
}
