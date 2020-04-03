using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player Controller")]
public class PlayerController : ControllerWrapper, IController
{
    public ActionKeyLinks[] actionLinks;
    ModelChar _model;

    public void AssignModel(ModelChar model)
    {
        _model = model;
    }

    public override ControllerWrapper Clone()
    {
        return new PlayerController();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < actionLinks.Length; i++)
        {
            if (Input.GetKey(actionLinks[i].key))
                if (actionLinks[i].action.action == null)
                    actionLinks[i].action.SetAction();
                else actionLinks[i].action.action.Do(_model);
        }
    }

    public override void SetController()
    {
        myController = this;
    }
}
