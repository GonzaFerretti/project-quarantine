using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/AI/ScoutAI")]
public class ScoutAI : ControllerWrapper, IController
{
    ModelPatrol _model;
    public Vector3 rotation;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
    }

    public override ControllerWrapper Clone()
    {
        ScoutAI clone = CreateInstance("ScoutAI") as ScoutAI;
        clone.rotation = rotation;
        return clone;
    }

    public void OnUpdate()
    {
        _model.transform.Rotate(rotation * Time.deltaTime);
    }

    public ScoutAI SetParams(NodeAction p)
    {
        rotation = new Vector3(0, p.rotationSpeed, 0);
        return this;
    }

    public IEnumerator RotationEnd()
    {
        yield return new WaitForSeconds(_model.node.queuedAction[_model.node.currentAction].secondsTillRotationEnd);

        _model.node.ChangeParams();
        _model.node.ChangeBehavior();
        if (_model.node.currentAction == _model.node.queuedAction.Length - 1)
        {
            _model.node.currentAction = 0;
        }
        else
        {
            _model.node.currentAction++;
        }
    }

    public override void SetController()
    {
        myController = this;
    }
}