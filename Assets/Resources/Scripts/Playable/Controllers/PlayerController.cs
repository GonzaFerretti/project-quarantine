using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/Player Controller")]
public class PlayerController : ControllerWrapper, IController
{
    public List<ActionKeyLinks> actionKeyLinks;

    protected ModelChar _model;

    public void AssignModel(Model model)
    {
        _model = model as ModelChar;
    }

    public override ControllerWrapper Clone()
    {
        return new PlayerController();
    }

    public virtual void OnUpdate()
    {
        CheckActionList(actionKeyLinks);
        List<ActionKeyLinks> addedActions = _model.gainedActionKeyLinks;
        CheckActionList(addedActions);
    }

    public override void SetController()
    {
        myController = this;
    }

    public void StartFunction()
    {
        ModelPlayable mp = _model as ModelPlayable;
        for (int i = 0; i < actionKeyLinks.Count; i++)
        {
            if (actionKeyLinks[i].myAction.action == null)
                actionKeyLinks[i].myAction.SetAction();
            if (actionKeyLinks[i].myAction.action is ActionMovement)
            {
                if (!mp.movementKeys.ContainsKey(actionKeyLinks[i].key))
                    mp.movementKeys[actionKeyLinks[i].key] = (actionKeyLinks[i].myAction.action as ActionMovement)._direction;
            }
        }
    }

    public void CheckActionList(List<ActionKeyLinks> actionList)
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            CheckAction(actionList[i]);
        }
    }

    public void CheckAction(ActionKeyLinks action)
    {
        if (action.CheckTrigger())
        {
            if (action.myAction.action == null)
                action.myAction.SetAction();
            action.myAction.action.Do(_model);
        }
    }
}
