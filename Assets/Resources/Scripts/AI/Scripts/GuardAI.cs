using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/AI/GuardAI")]
public class GuardAI : ControllerWrapper, IController
{
    ModelPatrol _model;
    Vector3 _target;
    public float speed;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
    }

    public override ControllerWrapper Clone()
    {
        GuardAI clone = CreateInstance("GuardAI") as GuardAI;
        clone.speed = speed;
        return clone;
    }

    public void OnUpdate()
    {
        Vector3 newDirection = Vector3.RotateTowards(_model.transform.forward, _target, speed * Time.deltaTime, 0.0f);
        _model.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public GuardAI SetParams(NodeAction p)
    {
        _target = p.targetView;
        return this;
    }

    public override void SetController()
    {
        myController = this;
    }
}
