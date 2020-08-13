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

    [Header("Light intensity variation")]
    public Light spyCamLight;
    public float minLightIntensity;
    public float maxLightIntensity;
    public float minLightScaleDistance;
    public float maxLightDistance;
    public GameObject areaPrefab;
    public FieldOfView area;

    public float maxIndicatorSize = -1;
    public float indicatorMaxSizeFactor;

    public float minIndicatorSize = -1;
    public float indicatorMinSizeFactor;

    protected override void Start()
    {
        startDir = transform.forward;
        leftForward = (Quaternion.Euler(0, camAngle / 2, 0) * transform.forward).normalized;
        rightForward = (Quaternion.Euler(0, -camAngle / 2, 0) * transform.forward).normalized;

        base.Start();
        controller = standardController;
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
        StartCoroutine(InitRangeIndicatorAfterAMoment());
    }

    IEnumerator InitRangeIndicatorAfterAMoment()
    {
        yield return new WaitForSeconds(1f);
        GameObject areago = Instantiate(areaPrefab, null);
        area = areago.GetComponent<FieldOfView>();
        area.Init(360, 1, gameObject, false);
        area.patrolMode = false;
        area.layerFilter = LayerMask.GetMask("Nothing");

    }

    protected override void Update()
    {
        base.Update();
        if (transform.hasChanged)
        {
            if (area)
            {
                UpdateLightIndicator();
            }
        }
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

    void UpdateLightIndicator()
    {
        RaycastHit angleHit = new RaycastHit();
        RaycastHit centerHit = new RaycastHit();
        Physics.Raycast(transform.position, (Quaternion.Euler(0, _angle, 0) * transform.forward).normalized, out angleHit, alertRange, 1 << 9);
        Physics.Raycast(transform.position, transform.forward, out centerHit, alertRange, 1 << 9);

        float radius = (angleHit.point - centerHit.point).magnitude;

        if (maxIndicatorSize == -1)
        {
            maxIndicatorSize = radius * indicatorMaxSizeFactor;
            minIndicatorSize = radius * indicatorMinSizeFactor;
        }
        float clampedRadius = Mathf.Clamp(radius, minIndicatorSize, maxIndicatorSize);
        area.transform.position = centerHit.point + area.heightOffset * Vector3.up;
        area.viewDistance = clampedRadius;
        transform.hasChanged = false;
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