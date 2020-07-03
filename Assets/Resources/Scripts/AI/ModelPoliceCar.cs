using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPoliceCar : ModelEnemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if(IsInSight(target, alertRange))
        {
            EventManager.TriggerEvent("Alert");
        }
    }
}
