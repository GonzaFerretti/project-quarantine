using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/Player Controller")]
public class PlayerController : ControllerWrapper, IController
{
    public ActionKeyLinks[] constantActionLinks;
    public ActionKeyLinks[] oneTimeActionLinks;

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
        for (int i = 0; i < constantActionLinks.Length; i++)
        {
            if (Input.GetKey(constantActionLinks[i].key))
            {
                if (constantActionLinks[i].action.action == null)
                    constantActionLinks[i].action.SetAction();
                constantActionLinks[i].action.action.Do(_model);

            }
        }

        for (int i = 0; i < oneTimeActionLinks.Length; i++)
        {
            if (Input.GetKeyDown(oneTimeActionLinks[i].key))
            {
                if (oneTimeActionLinks[i].action.action == null)
                    oneTimeActionLinks[i].action.SetAction();
                oneTimeActionLinks[i].action.action.Do(_model);
            }
        }
        List<ActionWrapper> addedActions = _model.availableActions;
        for (int i = 0; i < addedActions.Count; i++)
        {
            if (Input.GetKey(addedActions[i].actionKey.key))
                if (addedActions[i].actionKey.action.action == null)
                    addedActions[i].actionKey.action.SetAction();
                else addedActions[i].actionKey.action.action.Do(_model);
        }
    }

    public override void SetController()
    {
        myController = this;
    }

    public void StartFunction()
    {
        for (int i = 0; i < constantActionLinks.Length; i++)
        {

            if (constantActionLinks[i].action.action == null)
                constantActionLinks[i].action.SetAction();
            if (constantActionLinks[i].action.action is ActionMovement)
            {
                if (!movementKeys.Contains(constantActionLinks[i].key))
                    movementKeys.Add(constantActionLinks[i].key);
            }
        }
    }
}
