using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Talk")]
public class ActionTalkWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionTalk();
    }
}
