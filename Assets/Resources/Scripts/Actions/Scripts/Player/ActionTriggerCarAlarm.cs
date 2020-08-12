using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTriggerCarAlarm : BaseAction
{
    float _alarmRange;
    float _amountOfAlarms;
    float _totalAlarmTime;

    public ActionTriggerCarAlarm(float alarmRange, float amountOfAlarms, float totalAlarmTime)
    {
        _alarmRange = alarmRange;
        _amountOfAlarms = amountOfAlarms;
        _totalAlarmTime = totalAlarmTime;
    }
    
    public override void Do(Model m)
    {
        GameObject impactedObject = (m as FlingObject).impactedObject;
        if (impactedObject.GetComponent<ModelPoliceCar>())
        {
            ModelPoliceCar policeCar = impactedObject.GetComponent<ModelPoliceCar>();
            (m as FlingObject).hasMissed = false;
            (m as FlingObject).sm.Play(clip);
            if (policeCar.isDestroyed) return;
            policeCar.DestroyCar(_alarmRange,_amountOfAlarms,_totalAlarmTime);
        }
    }
}
