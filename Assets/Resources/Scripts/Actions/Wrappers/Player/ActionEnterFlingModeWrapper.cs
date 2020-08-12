using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Enter Fling Mode")]
public class ActionEnterFlingModeWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionEnterFlingMode();
        base.SetAction();
    }
}
