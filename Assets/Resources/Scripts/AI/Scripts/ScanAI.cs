using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/ScanAI")]
public class ScanAI : ControllerWrapper, IController
{
    ModelDrone _model;
    NavMeshAgent _agent;

    public void AssignModel(Model model)
    {
        _model = model as ModelDrone;
        _agent = model.GetComponent<NavMeshAgent>();
    }

    public override ControllerWrapper Clone()
    {
        ScanAI clone = CreateInstance("ScanAI") as ScanAI;
        return clone;
    }

    public void OnUpdate()
    {
        Vector3 rot = _model.transform.forward;
        Vector3 currentGoal;
        currentGoal = Vector3.RotateTowards(rot, (_model.target.transform.position - _model.transform.position).normalized, _model.currentSpeed * Time.deltaTime, 0);
        _model.transform.rotation = Quaternion.LookRotation(currentGoal);

        _agent.isStopped = true;
        //Vector3 dir = (_model.target.transform.position - _model.transform.position).normalized;
        ////Dif entre la distancia maxima del rango y el jugador
        //float mag = Vector3.Distance(_model.transform.position + dir * _model.alertRange, _model.target.transform.position);

        //Vector3 goal = (_model.transform.position + dir * _model.alertRange * mag - _model.target.transform.position);

        //_agent.SetDestination(goal);
    }

    public override void SetController()
    {
        myController = this;
    }
}