using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/StaggerAI")]
public class StaggerAI : ControllerWrapper, IController
{
    NavMeshAgent _agent;

    public void AssignModel(Model model)
    {
        _agent = model.GetComponent<NavMeshAgent>();
    }

    public override ControllerWrapper Clone()
    {
        StaggerAI clone = CreateInstance("StaggerAI") as StaggerAI;
        return clone;
    }

    public void OnUpdate()
    {
        _agent.isStopped = true;
    }

    public override void SetController()
    {
        myController = this;
    }
}