using System.Collections;
using UnityEngine;

public class ModelSpycam : ModelEnemy
{
    public float alertSpeed;
    public float waitTime;
    public float camAngle;

    public float goalRangeMargin;

    public bool coroutineCasted;

    public Vector3 startDir;
    public Vector3 rightForward;
    public Vector3 leftForward;
    public float test;
    public float test2;

    [Header("Light intensity variation")]
    public Light spyCamLight;
    public float minLightIntensity;
    public float maxLightIntensity;
    public float minLightScaleDistance;
    public float maxLightDistance;

    protected override void Start()
    {
        startDir = transform.forward;
        leftForward = (Quaternion.Euler(0, camAngle / 2, 0) * transform.forward).normalized;
        rightForward = (Quaternion.Euler(0, -camAngle / 2, 0) * transform.forward).normalized;

        test = transform.parent.localRotation.y + camAngle / 2;
        test2 = transform.parent.localRotation.y - camAngle / 2;

        base.Start();
        controller = standardController;
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target, alertRange))
        {
            EventManager.TriggerEvent("Alert");
            float distanceProgress = Mathf.InverseLerp(minLightScaleDistance, maxLightDistance, targetDistance);
            spyCamLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, distanceProgress);
        }
    }

    public IEnumerator Reactivate(float f)
    {
        yield return new WaitForSeconds(waitTime);
        currentSpeed = f;
        coroutineCasted = false;
    }

    void AlertBehavior()
    {
        if (this == null) return;
        StopAllCoroutines();
        coroutineCasted = false;
        currentSpeed = alertSpeed;
        controller = alertController;
    }

    void NormalBehavior()
    {
        if (this == null) return;
        currentSpeed = standardSpeed;
        controller = standardController;
    }

    void EnterBehavior()
    {
        EventManager.UnsubscribeToEvent("Alert", AlertBehavior);
        EventManager.UnsubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }
}