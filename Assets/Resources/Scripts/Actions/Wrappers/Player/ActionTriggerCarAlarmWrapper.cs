using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Trigger Car Alarm")]
public class ActionTriggerCarAlarmWrapper : ActionWrapper
{
    public float alarmRange;
    public float amountOfAlarms;
    public float alarmTime;

    public override void SetAction()
    {
        action = new ActionTriggerCarAlarm(alarmRange, amountOfAlarms, alarmTime);
        base.SetAction();
    }
}
