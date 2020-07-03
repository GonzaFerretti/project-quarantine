using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPatrol : ModelEnemy
{
    public PatrolNode node;
    public ControllerWrapper suspectController;
    public ControllerWrapper indoorController;
    public ControllerWrapper staggerController;
    public ControllerWrapper scoutController;
    public ControllerWrapper guardController;
    public PatrolSpawner spawner;
    public GameObject fovConePrefab;
    public float runSpeed;
    public Vector3 lastSight;
    public ActionCaptureWrapper actionCapture;
    public float meleeDistance;
    public float staggerTimer;

    protected override void Start()
    {
        suspectController = suspectController.Clone();
        (suspectController as IController).AssignModel(this);

        indoorController = indoorController.Clone();
        (indoorController as IController).AssignModel(this);

        staggerController = staggerController.Clone();
        (staggerController as IController).AssignModel(this);

        scoutController = scoutController.Clone();
        scoutController.SetController();
        (scoutController as IController).AssignModel(this);

        guardController = guardController.Clone();
        guardController.SetController();
        (guardController as IController).AssignModel(this);

        base.Start();
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToLocationEvent("Noise", SuspectNoise);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
        actionCapture = actionCapture.Clone();
        actionCapture.SetAction();

        InitFOVCone(_suspectRange, true);
        InitFOVCone(alertRange, false);
    }

    void InitFOVCone(float range, bool isSuspect)
    {
        GameObject cone = Instantiate(fovConePrefab, null);
        cone.GetComponent<FieldOfView>().Init(enemyAttributes.angle, range, gameObject, isSuspect);
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target, alertRange))
        {
            EventManager.TriggerEvent("Alert");
            (alertController as ChaseAI).timer = 0;
            (alertController as INeedTargetLocation).SetTarget(target.transform.position);
            if (!(controller is StaggerAI) && !(controller is ChaseAI))
            {
                Stagger(staggerTimer);
            }
        }
        if (IsInSight(target, _suspectRange))
        {
            SuspectBehavior();
            (suspectController as SuspectAI).SetTarget(target.transform.position);
        }
    }

    void NormalBehavior()
    {
        if (this == null) return;
        currentSpeed = standardSpeed;
        controller = standardController;
        animator.SetBool("running", false);
        animator.SetBool("walking", true);
        animator.SetBool("isIdle", false);

        if (controller.myController == null)
            controller.SetController();

        if (controller.myController is ExitIndoorAI)
            (controller.myController as ExitIndoorAI).myController.AssignModel(this);
        else
            (controller as PatrolAI).SetTarget();
    }

    void SuspectBehavior()
    {
        if (controller is ChaseAI) return;
        if (controller is SuspectAI) return;
        if (controller is StaggerAI) return;
        animator.SetBool("walking", true);
        animator.SetBool("running", false);
        animator.SetBool("isIdle", false);
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
        if (controller is ChaseAI) return;
        //Debug.Log(gameObject.name + "  " + Vector3.Distance(transform.position, target.transform.position) + " " + _alertDistance);
        if (Vector3.Distance(transform.position, target.transform.position) > _alertDistance) return;
        StartCoroutine(ChangeToAlert(0));
        lastSight = target.transform.position;
    }

    public void Stagger(float f)
    {
        if (staggerController.myController == null) staggerController.SetController();
        controller = staggerController;
        animator.SetBool("stagger",true);
        StartCoroutine(ChangeToAlert(f));
    }

    IEnumerator ChangeToAlert(float f)
    {
        yield return new WaitForSeconds(f);
        animator.SetBool("running", true);
        animator.SetBool("stagger", false);
        currentSpeed = runSpeed;
        controller = alertController;
        if (controller.myController == null)
            controller.SetController();
    }

    void EnterBehavior()
    {
        EventManager.UnsubscribeToEvent("Alert", AlertBehavior);
        EventManager.UnsubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.UnsubscribeToLocationEvent("Noise", SuspectNoise);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.GetComponent<ModelPlayable>())
        {
            if (controller == staggerController) return;
            if (controller != alertController)
            {
                Stagger(staggerTimer);
            }
            else
            {
                actionCapture.action.Do(this);
            }
        }
    }
}