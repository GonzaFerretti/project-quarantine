using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/ApproachGuardAI")]
public class ApproachGuardAI : ControllerWrapper, IController
{
    NavMeshAgent _agent;
    ModelPatrol _model;
    ModelPatrol _target;
    public float distanceThreshold;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
        _agent = _model.GetComponent<NavMeshAgent>();
    }

    public override ControllerWrapper Clone()
    {
        ApproachGuardAI clone = CreateInstance("ApproachGuardAI") as ApproachGuardAI;
        clone.distanceThreshold = distanceThreshold;
        return clone;
    }

    public void OnUpdate()
    {
        if(Vector3.Distance(_model.transform.position,_target.transform.position) <  distanceThreshold)
        {
            _target.GetComponentInChildren<RagdollController>().WakeUp();
            _model.controller = _model.standardController;
            if(_model.controller is PatrolAI)
            (_model.controller as PatrolAI).SetTarget();
        }
    }

    public void Relocate(Vector3 v)
    {
        _agent.SetDestination(v);
    }

    public void SetTarget(ModelPatrol target)
    {
        _target = target;
    }

    public override void SetController()
    {
        myController = this;
    }
}