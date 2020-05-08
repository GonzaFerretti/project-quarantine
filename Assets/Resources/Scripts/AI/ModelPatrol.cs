using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPatrol : ModelEnemy
{
    public PatrolNode node;
    public ControllerWrapper suspectController;

    public float runSpeed;
    public PatrolSpawner spawner;

    protected override void Start()
    {      
        suspectController = suspectController.Clone();
        (suspectController as IController).AssignModel(this);

        base.Start();
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToLocationEvent("Noise", SuspectNoise);
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target, alertRange)) EventManager.TriggerEvent("Alert");
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

        if (controller.myController == null)
            controller.SetController();
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
        (suspectController as SuspectAI).SetTarget(m.transform.position);
        SuspectBehavior();
    }

    void AlertBehavior()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > _alertDistance) return;
        Debug.Log("!");
        currentSpeed = runSpeed;
        controller = alertController;

        if (controller.myController == null)
            controller.SetController();
    }
}
