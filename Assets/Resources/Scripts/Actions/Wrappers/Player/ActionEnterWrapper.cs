using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Enter")]
public class ActionEnterWrapper : ActionBaseInteractWrapper
{
    public override void SetAction()
    {
        action = new ActionEnter(interactionDistance);
        base.SetAction();
    }
}
