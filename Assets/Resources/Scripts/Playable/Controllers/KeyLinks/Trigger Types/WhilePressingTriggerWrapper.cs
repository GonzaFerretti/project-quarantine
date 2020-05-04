using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/KeyTriggerType/WhilePressing")]
public class WhilePressingTriggerWrapper : TriggerType
{
    public override bool CheckTrigger(KeyCode key)
    {
        return Input.GetKey(key);
    }
}
