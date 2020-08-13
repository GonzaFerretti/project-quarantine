using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Controller/Action/Swap Flingable")]
public class ActionSwapFlingItemWrapper : ActionWrapper
{
    public FlingableItem bottle;
    public FlingableItem rock;

    public override void SetAction()
    {
        action = new ActionSwapFlingItem(bottle, rock);
        base.SetAction();
    }
}
