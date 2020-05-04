using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/KeyTriggerType/OnKeyUp")]
public class OnKeyUpTriggerWrapper : TriggerType
{
    public override bool CheckTrigger(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }
}
