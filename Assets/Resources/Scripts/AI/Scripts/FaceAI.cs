using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Controller/AI/FaceAI")]
public class FaceAI : ControllerWrapper, IController
{
    ModelChar _model;
    public void AssignModel(Model model)
    {
        _model = model as ModelChar;
    }

    public override ControllerWrapper Clone()
    {
        return CreateInstance("FaceAI") as FaceAI;
    }

    public void OnUpdate()
    {
        _model.transform.forward = ((_model as ModelEnemy).target.transform.position - _model.transform.position).normalized;
    }

    public override void SetController()
    {
        myController = this;
    }
}
