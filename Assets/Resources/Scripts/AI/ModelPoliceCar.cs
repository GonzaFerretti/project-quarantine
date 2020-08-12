using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPoliceCar : ModelEnemy, IMakeNoise
{
    private float noise;
    private float alarmTimes;
    private float alarmTime;
    public bool isDestroyed = false;
    public SoundClip sirenSound;

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

    public void DestroyCar(float noiseRange, float alarmTimes, float alartTime)
    {
        alertRange = 0;
        this.alarmTimes = alarmTimes;
        this.alarmTime = alartTime;
        noise = noiseRange;
        StartCoroutine(StartAlarmCoroutine());
        isDestroyed = true;
    }

    public float GetNoiseValue()
    {
        return noise;
    }

    IEnumerator StartAlarmCoroutine()
    {
        float alarmPeriod = alarmTime / alarmTimes;
        yield return new WaitForSeconds(alarmPeriod / 2);
        EventManager.TriggerLocEvent("Noise", this);
        sm.Play(sirenSound);
        yield return new WaitForSeconds(alarmPeriod / 2);
        while (alarmTimes > 0)
        {
            alarmTimes--;
            EventManager.TriggerLocEvent("Noise", this);
            sm.Play(sirenSound);
            yield return new WaitForSeconds(alarmPeriod);
        }
    }
}
