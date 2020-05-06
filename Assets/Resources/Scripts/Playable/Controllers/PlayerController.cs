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
<<<<<<< Updated upstream
            if (Input.GetKeyDown(oneTimeActionLinks[i].key))
=======

            if (actionKeyLinks[i].myAction.action == null)
                actionKeyLinks[i].myAction.SetAction();
            if (actionKeyLinks[i].myAction.action is ActionMovement)
>>>>>>> Stashed changes
            {
                if (oneTimeActionLinks[i].action.action == null)
                    oneTimeActionLinks[i].action.SetAction();
                oneTimeActionLinks[i].action.action.Do(_model);
            }
        }
    }

    public override void SetController()
    {
        myController = this;
    }

<<<<<<< Updated upstream
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
=======
    public void CheckAction(ActionKeyLinks links)
    {
        if (links.CheckTrigger())
        {
            if (links.myAction.action == null)
                links.myAction.SetAction();
            links.myAction.action.Do(_model);
>>>>>>> Stashed changes
        }
    }
}
