using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Duck Release")]
public class ActionDuckReleaseWrapper : ActionWrapper
{
    public float height;
    public Vector3 center;
    public override void SetAction()
    {
        action = new ActionDuckRelease(height,center);
    }
}
