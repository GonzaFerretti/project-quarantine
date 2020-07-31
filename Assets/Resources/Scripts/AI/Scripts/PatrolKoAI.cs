using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/AI/PatrolKoAI")]
public class PatrolKoAI : ControllerWrapper, IController
{
    ModelPatrol _model;

    public override void SetController()
    {
        myController = this;
    }

    public override ControllerWrapper Clone()
    {
        PatrolKoAI cloneWrapper = CreateInstance("PatrolKoAI") as PatrolKoAI;
        return cloneWrapper as ControllerWrapper;
    }

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
    }

    public void OnUpdate()
    {
    }
}
