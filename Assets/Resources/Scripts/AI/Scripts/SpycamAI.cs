using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/AI/SpycamAI")]
public class SpycamAI : ControllerWrapper, IController
{
    ModelSpycam _model;
    Vector3 _currentGoal;

    public void AssignModel(Model model)
    {
        _model = model as ModelSpycam;
        if (_currentGoal == Vector3.zero) _currentGoal = _model.leftForward;
    }

    public override ControllerWrapper Clone()
    {
        SpycamAI newSpycamAI = CreateInstance("SpycamAI") as SpycamAI;
        return newSpycamAI;
    }

    public void OnUpdate()
    {
        Vector3 currentGoal = Vector3.RotateTowards(_model.transform.forward, _currentGoal, _model.currentSpeed * Time.deltaTime, 0);
        _model.transform.rotation = Quaternion.LookRotation(currentGoal);

        ChangeAngle(_model.rightForward, _model.leftForward);
        ChangeAngle(_model.leftForward, _model.rightForward);
    }

    void ChangeAngle(Vector3 condition, Vector3 change)
    {
        if (IsInRange(condition, _model.goalRangeMargin))
        {
            _currentGoal = change;
            _model.currentSpeed = 0;
            if (!_model.coroutineCasted)
            {
                _model.StartCoroutine(_model.Reactivate(_model.standardSpeed));
                _model.coroutineCasted = true;
            }
        }
    }

    public bool IsInRange(Vector3 condition, float range)
    {
        if ((_model.transform.forward.x < condition.x + range && _model.transform.forward.x > condition.x - range) && (_model.transform.forward.z < condition.z + range && _model.transform.forward.z > condition.z - range))
            return true;
        else return false;
    }

    public override void SetController()
    {
        myController = this;
    }
}
