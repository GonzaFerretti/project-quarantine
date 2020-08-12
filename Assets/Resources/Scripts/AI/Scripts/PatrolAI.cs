using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/PatrolAI")]
public class PatrolAI : ControllerWrapper, IController
{
    ModelNodeUsingEnemy _model;
    public float nodeDistanceThreshold;
    NavMeshAgent _agent;

    public void AssignModel(Model model)
    {
        _model = model as ModelNodeUsingEnemy;
        _agent = _model.GetComponent<NavMeshAgent>();
    }

    public void OnUpdate()
    {
        //if (_model.node.queuedAction[_model.node.currentAction].action == null)
        //    _model.node.ActionChecker();


        //_model.node.queuedAction[_model.node.currentAction].action.Do(_model);

        if(_model.node.nextNode)
        {
            if (Vector3.Distance(_model.transform.position, _model.node.nextNode.transform.position) < nodeDistanceThreshold)
            {
                _model.node = _model.node.nextNode;
                _model.node.ChangeBehavior();
                _model.node.ChangeParams();
                if (_model.node.currentAction == _model.node.queuedAction.Length - 1)
                {
                    _model.node.currentAction = 0;
                }
                else
                {
                    _model.node.currentAction++;
                }
            }
        } 
    }

    public void SetTarget()
    {
        _agent.speed = _model.standardSpeed;
        _agent.SetDestination(_model.node.nextNode.transform.position);
    }

    public override void SetController()
    {
        myController = this;
    }

    public override ControllerWrapper Clone()
    {
        PatrolAI cloneWrapper = CreateInstance("PatrolAI") as PatrolAI;
        cloneWrapper.nodeDistanceThreshold = nodeDistanceThreshold;
        return cloneWrapper as ControllerWrapper;
    }
}