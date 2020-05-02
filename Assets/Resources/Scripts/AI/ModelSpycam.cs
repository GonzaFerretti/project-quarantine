using System.Collections;
using UnityEngine;

public class ModelSpycam : ModelEnemy
{
    public float changeFrequency;
    public float limit;

    public float waitTime;
    SpycamAI spycamAI;

    protected override void Start()
    {
        base.Start();
        spycamAI = controller as SpycamAI;

        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        StartCoroutine(Deactivate(changeFrequency/2));
    }

    protected override void Update()
    {
        base.Update();

        if (IsInSight(target, _alertRange)) EventManager.TriggerEvent("Alert");
    }

    public IEnumerator Deactivate(float f)
    {
        yield return new WaitForSeconds(f);
        StartCoroutine(Reactivate(-currentSpeed));
        currentSpeed = 0;
    }

    public IEnumerator Reactivate(float f)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Deactivate(changeFrequency));
        currentSpeed = f;
    }

    void AlertBehavior()
    {
        controller = alertController;
    }

    void NormalBehavior()
    {
        controller = standardController;
    }
}
