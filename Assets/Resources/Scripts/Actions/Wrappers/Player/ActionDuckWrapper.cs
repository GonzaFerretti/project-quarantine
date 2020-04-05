using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Duck")]
public class ActionDuckWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionDuck();
    }
}
