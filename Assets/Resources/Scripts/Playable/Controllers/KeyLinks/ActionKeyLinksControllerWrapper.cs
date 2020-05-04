using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Controller/Action + Key Link")]
public class ActionKeyLinkControllerWrapper : ScriptableObject
{
    public ActionWrapper _action;
    public KeyCode _key;
    public TriggerType _triggerType;

    public ActionKeyLinkControllerWrapper(ActionWrapper action, KeyCode key, TriggerType triggerType)
    {
        _action = action;
        _key = key;
        _triggerType = triggerType;
    }

    public bool CheckTrigger()
    {
        return _triggerType.CheckTrigger(_key);
    }
    // If changekey will be used to allow the end user to change key bindings, we shouldn't allow them to modify also the trigger type, so it was omitted in the parameters.
    public void ChangeKey(KeyCode newKey)
    {
        _key = newKey;
    }
}
