using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelEnemy : ModelChar
{
    public ControllerWrapper standardController;
    public ControllerWrapper alertController;
    public EnemyAttributes enemyAttributes;
    public float alertRange;
    public float headHeight;
    protected float _suspectRange;
    protected float _angle;
    protected float _alertDistance;
    public LayerMask visibility;
    public ModelPlayable target;
    public float targetDistance = 0;

    //Tentative
    private Transform debugTarget;

    protected override void Start()
    {
        standardController = standardController.Clone();
        if (controller == null)
            controller = standardController;
        base.Start();
        alertController = alertController.Clone();
        alertController.SetController();
        (alertController as IController).AssignModel(this);
        SetAttributes();

        //Tentative
        if (!target) target = FindObjectOfType<ModelPlayable>();
    }

    void SetAttributes()
    {
        alertRange = enemyAttributes.alertRange;
        _suspectRange = enemyAttributes.suspectRange;
        _angle = enemyAttributes.angle;
        _alertDistance = enemyAttributes.alertDistance;
        InitModel(ref animator, enemyAttributes.characterModel, enemyAttributes.animations);
    }

    public bool IsInSight(ModelPlayable player, float range)
    {
        Vector3 head = new Vector3(0, headHeight, 0);
        Vector3 playerHead = new Vector3(0, player.bodyHeight, 0);
        debugTarget = player.transform;
        Vector3 positionDifference = (player.transform.position + playerHead) - (transform.position + head);

        float distance = positionDifference.magnitude;

        if (distance > range) return false;

        var angleToTarget = Vector3.Angle(transform.forward, positionDifference);

        if (angleToTarget > _angle / 2) return false;
        
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position + head, positionDifference, out hitInfo, range, visibility))
        {
            if (hitInfo.transform != player.transform) return false;            
            if (hitInfo.transform == player.transform && player.isHidden) return false;
            targetDistance = (hitInfo.transform.position - transform.position).magnitude;
        }
        return true;
    }

        //void OnDrawGizmos()
        //{
        //    var position = transform.position + new Vector3(0, headHeight, 0);

        //    //Gizmos.color = Color.white;
        //    //Gizmos.DrawWireSphere(position, _suspectRange);

        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawLine(position, position + Quaternion.Euler(0, -_angle / 2, 0) * transform.forward * alertRange);
        //    Gizmos.DrawLine(position, position + Quaternion.Euler(0, _angle / 2, 0) * transform.forward * alertRange);


        //    if (target)
        //        Gizmos.DrawLine(position, target.transform.position + new Vector3(0, target.bodyHeight, 0));
        //}
    }
