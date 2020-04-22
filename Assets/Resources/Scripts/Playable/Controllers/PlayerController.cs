using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player Controller")]
public class PlayerController : ControllerWrapper, IController
{
    public ActionKeyLinks[] actionLinks;
    ModelChar _model;
    public List<KeyCode> movementKeys = new List<KeyCode>();
    //public ActionWrapper[] passiveActions;

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

        //for (int i = 0; i < passiveActions.Length; i++)
        //{
        //    passiveActions[i].action.Do(_model);
        //}
    }

    public override void SetController()
    {
        myController = this;
    }

    public void StartFunction()
    {
        for (int i = 0; i < actionLinks.Length; i++)
        {

            if (actionLinks[i].action.action == null)
                actionLinks[i].action.SetAction();
            if (actionLinks[i].action.action is ActionMovement)
            {
                if (!movementKeys.Contains(actionLinks[i].key))
                    movementKeys.Add(actionLinks[i].key);
            }
        }
    }
}
