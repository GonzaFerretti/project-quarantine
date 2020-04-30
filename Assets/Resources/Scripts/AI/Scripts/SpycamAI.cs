using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/AI/SpycamAI")]
public class SpycamAI : ControllerWrapper, IController
{
    ModelChar _model;

    public void AssignModel(Model model)
    {
        _model = model as ModelChar;
    }

    public override ControllerWrapper Clone()
    {
        return CreateInstance("SpycamAI") as SpycamAI;
    }

    public void OnUpdate()
    {
        _model.transform.Rotate(0, _model.currentSpeed * Time.deltaTime, 0);
    }

    public override void SetController()
    {
        myController = this;
    }
}
