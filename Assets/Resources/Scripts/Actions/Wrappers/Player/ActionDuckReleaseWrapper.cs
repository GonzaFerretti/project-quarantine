using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/DuckRelease")]
public class ActionDuckReleaseWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionDuckRelease();
    }
}

