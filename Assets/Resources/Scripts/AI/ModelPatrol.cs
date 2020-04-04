using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPatrol : ModelChar
{
    public PatrolNode node;
    public float range;
    public float angle;
    public float alertDuration;
    public float alertCountdown;
    public SpriteRenderer FOVcone;
    public LayerMask visibility;
    public ModelPlayable target;
    public ControllerWrapper patrolcontroller;
    public ControllerWrapper alertcontroller;

    private Transform debugTarget;

    protected override void Start()
    {
        controller = patrolcontroller;
        base.Start();
        EventManager.SubscribeToEvent("Alert", AlertBehavior);
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target)) EventManager.TriggerEvent("Alert");
    }

    void AlertBehavior()
    {
        Debug.Log("!");
        currentSpeed = runSpeed;
        FOVcone.color = new Color(1, 0, 0);
        controller = alertcontroller;
        
        if(controller.myController == null)
        controller.SetController();
    }

    public bool IsInSight(ModelPlayable player)
    {
        debugTarget = player.transform;

        Vector3 positionDifference = player.transform.position - transform.position;

        float distance = positionDifference.magnitude;

        if (distance > range) return false;

        var angleToTarget = Vector3.Angle(transform.forward, positionDifference);

        if (angleToTarget > angle / 2) return false;

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, positionDifference, out hitInfo, range, visibility))
        {
            if (hitInfo.transform != player.transform) return false;
        }

        return true;
    }

    void OnDrawGizmos()
    {
        var position = transform.position;

        Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(position, range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

        if (debugTarget)
            Gizmos.DrawLine(position, debugTarget.position);
    }

}
