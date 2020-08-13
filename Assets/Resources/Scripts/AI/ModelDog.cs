using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ModelDog : ModelNodeUsingEnemy
{
    public ScentCreator scentCreator;
    public ControllerWrapper suspectController;
    public ControllerWrapper staggerController;
    public ActionBiteWrapper biteAction;
    public int currentScentTrail;
    public float staggerTimer;
    public float runSpeed;
    public SoundClip dogBark;
    public SoundClip dogGrowl;

    protected override void Start()
    {
        biteAction = biteAction.Clone();
        biteAction.SetAction();
        suspectController = suspectController.Clone();
        suspectController.SetController();
        (suspectController as IController).AssignModel(this);

        staggerController = staggerController.Clone();
        staggerController.SetController();
        (staggerController as IController).AssignModel(this);

        base.Start();

        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);

        scentCreator = target.GetComponentInChildren<ScentCreator>();
        animator.SetBool("running", true);
        StartCoroutine(FoVConeIitialization());
    }

    protected override void Update()
    {
        base.Update();

        if (IsInSight(target, alertRange))
        {
            (alertController as ChaseAI).timer = 0;
            (alertController as INeedTargetLocation).SetTarget(target.transform.position);
            if (!(controller is StaggerAI) && !(controller is ChaseAI))
            {
                Stagger(staggerTimer);
            }
            sm.Play(dogBark);
            EventManager.TriggerEvent("Alert");
        }

        if (IsInSight(target, _suspectRange)) return;
        if (controller is SniffingAI) return;
        for (int i = 0; i < scentCreator.scentObjects.Count; i++)
        {
            if (IsInSmellRange(scentCreator.scentObjects[i], _suspectRange))
            {
                (suspectController as SniffingAI).SetTarget(scentCreator.scentObjects[i].transform.position);
                (suspectController as SniffingAI).Move();
                currentScentTrail = i;
                controller = suspectController;
                break;
            }
        }
    }

    public void Stagger(float f)
    {
        if (staggerController.myController == null) staggerController.SetController();
        controller = staggerController;
        animator.SetBool("running", false);
        StartCoroutine(ChangeToAlert(f));
    }

    IEnumerator ChangeToAlert(float f)
    {
        yield return new WaitForSeconds(f);
        animator.SetBool("running", true);
        currentSpeed = runSpeed;
        controller = alertController;
        if (controller.myController == null)
            controller.SetController();
    }

    public bool IsInSmellRange(ModelScentTrail scentTrail, float range)
    {
        if (!scentTrail) return false;
        Vector3 head = new Vector3(0, headHeight, 0);
        Vector3 positionDifference = (scentTrail.transform.position) - (transform.position + head);

        float distance = positionDifference.magnitude;

        if (distance > range) return false;

        var angleToTarget = Vector3.Angle(transform.forward, positionDifference);

        if (angleToTarget > _angle / 2) return false;

        RaycastHit hitInfo;

        for (int i = 0; i < scentCreator.scentTrailAmount; i++)
        {
            if (Physics.Raycast(transform.position + head, positionDifference.normalized, out hitInfo, range))
            {
                if (hitInfo.transform != scentTrail.transform) return false;
                targetDistance = (hitInfo.transform.position - transform.position).magnitude;
            }
        }
        return true;
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.GetComponent<ModelPlayable>())
        {
            if (controller != alertController)
            {
                Stagger(staggerTimer);
            }
        }
    }

    void AlertBehavior()
    {
        if (controller is ChaseAI) return;
        if (controller is StaggerAI) return;
        if (Vector3.Distance(transform.position, target.transform.position) > _alertDistance) return;
        Stagger(staggerTimer);
        lastSight = target.transform.position;
    }

    void NormalBehavior()
    {
        if (this == null) return;
        currentSpeed = standardSpeed;
        controller = standardController;

        if (controller.myController == null)
            controller.SetController();

        (controller as PatrolAI).SetTarget();
    }

    void EnterBehavior()
    {
        EventManager.UnsubscribeToEvent("Alert", AlertBehavior);
        EventManager.UnsubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<ModelPlayable>())
        {
            if (controller != alertController)
            {
                Stagger(staggerTimer);
            }
            else
            {
                sm.Play(dogGrowl);
                biteAction.action.Do(this);
            }
        }
    }
}