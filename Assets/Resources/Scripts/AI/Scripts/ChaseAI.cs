using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/ChaseAI")]
public class ChaseAI : ControllerWrapper, IController
{
    ModelPatrol _model;

    public void AssignModel(ModelChar model)
    {
        _model = model as ModelPatrol;
    }

    public override ControllerWrapper Clone()
    {
        return CreateInstance("ChaseAI") as ControllerWrapper;
    }

    public void OnUpdate()
    {
        _model.GetComponent<NavMeshAgent>().SetDestination(_model.target.transform.position);
    }

    public override void SetController()
    {
        myController = this;
    }    
}
