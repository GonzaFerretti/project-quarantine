using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Controller/Action/Start Whistle")]
public class ActionStartWhistleWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionStartWhistle();
    }
}
