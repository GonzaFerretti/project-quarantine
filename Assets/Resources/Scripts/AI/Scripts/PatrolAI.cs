using UnityEngine;

[CreateAssetMenu(menuName = "Controller/AI/PatrolAI")]
public class PatrolAI : ControllerWrapper, IController
{
    ModelPatrol _model;
    public float nodeDistanceThreshold;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
    }

    public void OnUpdate()
    {
        if (_model.node.queuedAction[_model.node.currentAction].action == null)
            _model.node.ActionChecker();
        _model.node.queuedAction[_model.node.currentAction].action.Do(_model);

        if (Vector3.Distance(_model.transform.position, _model.node.nextNode.transform.position) < nodeDistanceThreshold)
        {
            if (_model.node.currentAction == _model.node.queuedAction.Length - 1) _model.node.currentAction = 0;
            _model.node = _model.node.nextNode;
        }
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
