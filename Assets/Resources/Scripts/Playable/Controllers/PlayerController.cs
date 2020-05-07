using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/Player Controller")]
public class PlayerController : ControllerWrapper, IController
{
    public List<ActionKeyLinks> actionKeyLinks;

    ModelChar _model;
    public List<KeyCode> movementKeys = new List<KeyCode>();

    public void AssignModel(Model model)
    {
        _model = model as ModelChar;
    }

    public override ControllerWrapper Clone()
    {
        return new PlayerController();
    }

    public void OnUpdate()
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
        for (int i = 0; i < actionKeyLinks.Count; i++)
        {
            if (actionKeyLinks[i].myAction.action == null)
                actionKeyLinks[i].myAction.SetAction();
            if (actionKeyLinks[i].myAction.action is ActionMovement)
            {
                if (!movementKeys.Contains(actionKeyLinks[i].key))
                    movementKeys.Add(actionKeyLinks[i].key);
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
            //try { 
            action.myAction.action.Do(_model);
            /*}
            catch
            {
                Debug.Log(action);
            }*/
        }
    }
}
