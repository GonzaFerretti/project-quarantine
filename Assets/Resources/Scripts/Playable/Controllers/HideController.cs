using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/Hide Controller")]
public class HideController : ControllerWrapper, IController
{
    ModelChar _model;
    public ActionKeyLinks[] actionLinks;

    public void AssignModel(Model model)
    {
        _model = model as ModelChar;
    }

    public override ControllerWrapper Clone()
    {
        return new HideController();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < actionLinks.Length; i++)
        {
            if (Input.GetKey(actionLinks[i].key))
                if (actionLinks[i].myAction.action == null)
                    actionLinks[i].myAction.SetAction();
                else actionLinks[i].myAction.action.Do(_model);
        }
    }

    public override void SetController()
    {
        myController = this;
    }
}
