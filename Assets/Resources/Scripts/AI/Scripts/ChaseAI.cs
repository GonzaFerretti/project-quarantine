using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/ChaseAI")]
public class ChaseAI : ControllerWrapper, IController, INeedTargetLocation
{
    public float targetTreshold;
    ModelPatrol _model;
    Vector3 _target;
    public float maxTimer;
    public float timer;
    public SearchAI searchAI;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
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
        {
            if (timer < maxTimer)
            {
                _model.GetComponent<NavMeshAgent>().SetDestination(_model.target.transform.position);
                timer += 1 * Time.deltaTime;
            }
            else
            {
                if(Vector3.Distance(_model.transform.position, _model.lastSight) < targetTreshold)
                {
                    (searchAI as IController).AssignModel(_model);
                    searchAI.GenerateNewGoal();
                    _model.controller = searchAI;
                }else
                _model.GetComponent<NavMeshAgent>().SetDestination(_model.lastSight);
            }

            _model.animator.SetBool("running", true);
        }
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
