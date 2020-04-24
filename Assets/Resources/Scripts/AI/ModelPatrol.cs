using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPatrol : ModelChar
{
    public PatrolNode node;
    float _alertRange;
    float _suspectRange;
    float _angle;
    float _alertDistance;
    float _hearingDistance;
    public LayerMask visibility;
    public ModelPlayable target;
    public ControllerWrapper patrolController;
    public ControllerWrapper suspectController;
    public ControllerWrapper alertController;
    public EnemyAttributes enemyAttributes;

    private Transform debugTarget;

    //Tentative
    public Material mat;

    protected override void Start()
    {
        mat = Resources.Load<Material>("Art/Visual/Placeholder/Red");
        transform.GetComponent<MeshRenderer>().material = mat;
        SetAttributes();
        patrolController = patrolController.Clone();
        suspectController = suspectController.Clone();
        alertController = alertController.Clone();
        controller = patrolController;
        base.Start();
        (alertController as IController).AssignModel(this);
        (suspectController as IController).AssignModel(this);
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target, _alertRange)) EventManager.TriggerEvent("Alert");
        if (IsInSight(target, _suspectRange))
        {
            SuspectBehavior();
            (suspectController as SuspectAI).SetTarget(target.transform.position);
        }
    }

    void SetAttributes()
    {
        _alertRange = enemyAttributes.alertRange;
        _suspectRange = enemyAttributes.suspectRange;
        _angle = enemyAttributes.angle;
        _alertDistance = enemyAttributes.alertDistance;
        _hearingDistance = enemyAttributes.hearingDistance;
        GetComponent<MeshFilter>().mesh = enemyAttributes.mesh;
    }

    void NormalBehavior()
    {
        currentSpeed = walkSpeed;
        controller = patrolController;

        if (controller.myController == null)
            controller.SetController();
    }

    void SuspectBehavior()
    {
        if (controller is ChaseAI) return;

        Debug.Log("?");
        currentSpeed = walkSpeed;
        controller = suspectController;

        if (controller.myController == null)
            controller.SetController();
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

    public bool IsInSight(ModelPlayable player, float range)
    {
        debugTarget = player.transform;

        Vector3 positionDifference = player.transform.position - transform.position;

        float distance = positionDifference.magnitude;

        if (distance > range) return false;

        var angleToTarget = Vector3.Angle(transform.forward, positionDifference);

        if (angleToTarget > _angle / 2) return false;

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, positionDifference, out hitInfo, range, visibility))
        {
            if (hitInfo.transform != player.transform) return false;
        }
        return true;
    }

    //void OnDrawGizmos()
    //{
    //    var position = transform.position;

    //    Gizmos.color = Color.white;
    //    Gizmos.DrawWireSphere(position, _suspectRange);

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(position, position + Quaternion.Euler(0, -_angle / 2, 0) * transform.forward * _suspectRange);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(position, position + Quaternion.Euler(0, _angle / 2, 0) * transform.forward * _alertRange);

    //    if (debugTarget)
    //        Gizmos.DrawLine(position, debugTarget.position);
    //}

}
