using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Dump Body")]
public class ActionDumpBodyWrapper : ActionDropBodyWrapper
{
    public override void SetAction()
    {
        //action = new ActionDumpBody(height,radius,center);
    }
}
