using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/KeyTriggerType/OnKeyDown")]
public class OnKeyDownTriggerWrapper : TriggerType
{
    public override bool CheckTrigger(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }
}
