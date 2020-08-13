using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Drop Body")]
public class ActionDropBodyWrapper : ActionWrapper
{
    public Item dragUI;
    public override void SetAction()
    {
        action = new ActionDropBody(dragUI);
        base.SetAction();
    }
}
