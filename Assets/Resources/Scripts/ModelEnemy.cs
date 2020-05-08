using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelEnemy : ModelChar
{
    public ControllerWrapper standardController;
    public ControllerWrapper alertController;
    public EnemyAttributes enemyAttributes;
    public float alertRange;
    protected float _suspectRange;
    protected float _angle;
    protected float _alertDistance;
    protected float _hearingDistance;
    public LayerMask visibility;
    public ModelPlayable target;

    //Tentative
    public Material mat;
    private Transform debugTarget;

    protected override void Start()
    {
        standardController = standardController.Clone();
        if(controller == null)
        controller = standardController;
        base.Start();
        alertController = alertController.Clone();
        alertController.SetController();
        (alertController as IController).AssignModel(this);
        TentativeColorSwap();
        SetAttributes();

        //Tentative
        if (!target) target = FindObjectOfType<ModelPlayable>();
    }

    void TentativeColorSwap()
    {
        mat = Resources.Load<Material>("Art/Visual/Placeholder/Red");
        if (transform.GetComponent<MeshRenderer>())
                transform.GetComponent<MeshRenderer>().material = mat;
        else transform.GetComponentInChildren<MeshRenderer>().material = mat;
    }

    void SetAttributes()
    {
        alertRange = enemyAttributes.alertRange;
        _suspectRange = enemyAttributes.suspectRange;
        _angle = enemyAttributes.angle;
        _alertDistance = enemyAttributes.alertDistance;
        _hearingDistance = enemyAttributes.hearingDistance;

        if (GetComponent<MeshFilter>())
        GetComponent<MeshFilter>().mesh = enemyAttributes.mesh;
        else GetComponentInChildren<MeshFilter>().mesh = enemyAttributes.mesh;
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
    //    Gizmos.DrawLine(position, position + Quaternion.Euler(0, -_angle / 2, 0) * transform.forward * _alertRange);
    //    Gizmos.DrawLine(position, position + Quaternion.Euler(0, _angle / 2, 0) * transform.forward * _alertRange);


    //    if (debugTarget)
    //        Gizmos.DrawLine(position, debugTarget.position);
    //}

}
