using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPatrol : ModelNodeUsingEnemy
{
    public ControllerWrapper suspectController;
    public ControllerWrapper indoorController;
    public ControllerWrapper staggerController;
    public ControllerWrapper koController;
    public ControllerWrapper lookAtFlungObjectController;
    public ControllerWrapper approachGuardAI;
    public PatrolSpawner spawner;
    public float runSpeed;
    public ActionCaptureWrapper actionCapture;
    public float meleeDistance;
    public float staggerTimer;
    public RagdollController ragdoll;
    public KOPatrolManager KOManager;

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

        approachGuardAI = approachGuardAI.Clone();
        approachGuardAI.SetController();
        (approachGuardAI as IController).AssignModel(this);

        koController = koController.Clone();
        koController.SetController();
        (koController as IController).AssignModel(this);

        lookAtFlungObjectController = lookAtFlungObjectController.Clone();
        lookAtFlungObjectController.SetController();
        (lookAtFlungObjectController as IController).AssignModel(this);

        base.Start();
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToLocationEvent("Noise", NoiseChecker);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
        actionCapture = actionCapture.Clone();
        actionCapture.SetAction();
        ragdoll = GetComponentInChildren<RagdollController>();
        ragdoll.Init(this);
        StartCoroutine(FoVConeIitialization());
        KOManager = FindObjectOfType<KOPatrolManager>();
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target, alertRange))
        {
            if (controller is PatrolKoAI) return;
            (alertController as ChaseAI).timer = 0;
            (alertController as INeedTargetLocation).SetTarget(target.transform.position);
            if (!(controller is StaggerAI) && !(controller is ChaseAI))
            {
                Stagger(staggerTimer);
            }
            EventManager.TriggerEvent("Alert");
        }
        if (IsInSight(target, _suspectRange))
        {
            SuspectBehavior();
            (suspectController as SuspectAI).SetTarget(target.transform.position);
        }

        if (KOManager.KOPatrolList.Count > 0)
        {
            for (int i = 0; i < KOManager.KOPatrolList.Count; i++)
            {
                if (IsKOPatrolInSight(KOManager.KOPatrolList[i], _suspectRange))
                {
                    controller = approachGuardAI;
                    (controller as ApproachGuardAI).SetTarget(KOManager.KOPatrolList[i]);
                    (controller as ApproachGuardAI).Relocate(KOManager.KOPatrolList[i].transform.position);
                }
            }
        }
    }

    public void EnableEvents()
    {
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToLocationEvent("Noise", NoiseChecker);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    public void DisableEvents()
    {

        EventManager.UnsubscribeToEvent("Alert", AlertBehavior);
        EventManager.UnsubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.UnsubscribeToLocationEvent("Noise", NoiseChecker);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    public bool IsKOPatrolInSight(ModelPatrol patrol, float range)
    {
        //if (!(controller is PatrolAI)) return false;
        if (IsInSight(target, _suspectRange)) return false;

        Vector3 head = new Vector3(0, headHeight, 0);
        Vector3 patrolHead = new Vector3(0, patrol.headHeight / 2, 0);
        Vector3 positionDifference = (patrol.transform.position + patrolHead) - (transform.position + head);

        float distance = positionDifference.magnitude;

        if (distance > range) return false;

        var angleToTarget = Vector3.Angle(transform.forward, positionDifference);

        if (angleToTarget > _angle / 2) return false;

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position + head, positionDifference, out hitInfo, range, visibility))
        {
            if (hitInfo.transform.GetComponentInParent<ModelPatrol>() != patrol) return false;
        }
        return true;
    }


    void NormalBehavior()
    {
        if (this == null) return;
        currentSpeed = standardSpeed;
        controller = standardController;
        animator.SetBool("running", false);
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
        if (controller is PatrolKoAI) return;
        animator.SetBool("running", false);
        animator.SetBool("isIdle", false);
        currentSpeed = standardSpeed;
        controller = suspectController;

        if (controller.myController == null)
            controller.SetController();
    }

    void NoiseChecker(Model m)
    {
        if (!(m is IMakeNoise)) return;
        if (controller is PatrolKoAI) return;

        IMakeNoise mn = (m as IMakeNoise);
        if ((mn.GetNoiseValue() - Vector3.Distance(transform.position, m.transform.position)) < 0) return;

        Vector3 newLocation = m.transform.position + (m.transform.position - transform.position).normalized;

        (lookAtFlungObjectController as INeedTargetLocation).SetTarget(newLocation);
        NoiseBehavior();
    }

    void NoiseBehavior()
    {
        if (controller is ChaseAI) return;
        if (controller is SuspectAI) return;
        if (controller is StaggerAI) return;
        if (controller is PatrolKoAI) return;
        animator.SetBool("running", false);
        animator.SetBool("isIdle", false);
        currentSpeed = standardSpeed;
        controller = lookAtFlungObjectController;

        if (controller.myController == null)
            controller.SetController();
    }

    void AlertBehavior()
    {
        if (controller is ChaseAI) return;
        if (controller is StaggerAI) return;
        if (controller is PatrolKoAI) return;
        //Debug.Log(gameObject.name + "  " + Vector3.Distance(transform.position, target.transform.position) + " " + _alertDistance);
        if (Vector3.Distance(transform.position, target.transform.position) > _alertDistance) return;
        Stagger(staggerTimer);
        lastSight = target.transform.position;
    }

    public void Stagger(float f)
    {
        if (staggerController.myController == null) staggerController.SetController();
        controller = staggerController;
        animator.SetBool("running", false);
        animator.SetBool("stagger", true);
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
        EventManager.UnsubscribeToLocationEvent("Noise", NoiseChecker);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (controller is StaggerAI) return;
            if (controller is PatrolKoAI) return;
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