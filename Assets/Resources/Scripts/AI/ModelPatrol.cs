using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPatrol : ModelEnemy
{
    public PatrolNode node;
    public ControllerWrapper suspectController;
    public ControllerWrapper indoorController;
    public PatrolSpawner spawner;
    public float runSpeed;
    public Vector3 lastSight;
    public ActionCaptureWrapper actionCapture;
    public float meleeDistance;

    protected override void Start()
    {
        suspectController = suspectController.Clone();
        (suspectController as IController).AssignModel(this);

        indoorController = indoorController.Clone();
        (indoorController as IController).AssignModel(this);

        base.Start();
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToLocationEvent("Noise", SuspectNoise);
        actionCapture = actionCapture.Clone();
        actionCapture.SetAction();
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target, alertRange))
        {
            EventManager.TriggerEvent("Alert");
            (alertController as ChaseAI).timer = 0;
            (alertController as INeedTargetLocation).SetTarget(target.transform.position);
        }
        if (IsInSight(target, _suspectRange))
        {
            SuspectBehavior();
            (suspectController as SuspectAI).SetTarget(target.transform.position);
        }
    }

    void NormalBehavior()
    {
        currentSpeed = standardSpeed;
        controller = standardController;
        animator.SetBool("running", false);

        if (controller.myController == null)
            controller.SetController();

        if (controller.myController is ExitIndoorAI)
            (controller.myController as ExitIndoorAI).myController.AssignModel(this);
    }

    void SuspectBehavior()
    {
        if (controller is ChaseAI) return;

        Debug.Log("?");
        currentSpeed = standardSpeed;
        controller = suspectController;

        if (controller.myController == null)
            controller.SetController();
    }

    void SuspectNoise(Model m)
    {
        if (!(m is IMakeNoise)) return;

        IMakeNoise mn = (m as IMakeNoise);

        if ((mn.GetNoiseValue() - Vector3.Distance(transform.position, m.transform.position)) < 0) return;

        (suspectController as INeedTargetLocation).SetTarget(m.transform.position);
        SuspectBehavior();

    }

    void AlertBehavior()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > _alertDistance) return;
        Debug.Log("!");
        currentSpeed = runSpeed;
        controller = alertController;
        lastSight = target.transform.position;
        if (controller.myController == null)
            controller.SetController();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<ModelPlayable>())
        {
            if (controller != alertController)
            {
                SuspectBehavior();
                (suspectController as SuspectAI).SetTarget(target.transform.position);
            }
            else
            actionCapture.action.Do(this);
        }
    }
}
