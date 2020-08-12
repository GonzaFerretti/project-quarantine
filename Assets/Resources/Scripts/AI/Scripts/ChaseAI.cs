using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/ChaseAI")]
public class ChaseAI : ControllerWrapper, IController, INeedTargetLocation
{
    public SearchAI searchAI;
    public float targetTreshold;
    public float maxTimer;
    public float timer;
    NavMeshAgent _agent;
    ModelNodeUsingEnemy _model;
    Vector3 _target;

    public void AssignModel(Model model)
    {
        _model = model as ModelNodeUsingEnemy;
        _agent = model.GetComponent<NavMeshAgent>();
    }

    public override ControllerWrapper Clone()
    {
        ChaseAI clone = CreateInstance("ChaseAI") as ChaseAI;
        clone.targetTreshold = targetTreshold;
        clone.maxTimer = maxTimer;
        clone.searchAI = searchAI.Clone() as SearchAI;
        clone.searchAI.SetController();
        return clone;
    }

    public void OnUpdate()
    {
        if (_agent.isStopped) _agent.isStopped = false;

        if (timer < maxTimer)
        {
            _agent.speed = _model.currentSpeed;
            _agent.SetDestination(_model.target.transform.position);
            timer += 1 * Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(_model.transform.position, _model.lastSight) < targetTreshold)
            {
                (searchAI as IController).AssignModel(_model);
                searchAI.GenerateNewGoal();
                _model.controller = searchAI;
            }
            else
                _agent.SetDestination(_model.lastSight);
        }

        if(_model.animator.GetBool("running"))
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