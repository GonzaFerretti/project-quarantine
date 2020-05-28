using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/ChaseAI")]
public class ChaseAI : ControllerWrapper, IController, INeedTargetLocation
{
    public float targetTreshold;
    ModelPatrol _model;
    Vector3 _target;

    public void AssignModel(Model model)
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
        _model.animator.SetBool("running", true);
    }

    bool Distance()
    {
        return Vector3.Distance(_model.transform.position, _target) > targetTreshold;
    }

    public override void SetController()
    {
        myController = this;
    }

    public INeedTargetLocation SetTarget(Vector3 target)
    {
        _target = target;
        return this;
    }
}
