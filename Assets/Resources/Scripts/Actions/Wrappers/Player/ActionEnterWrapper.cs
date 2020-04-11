using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Enter")]
public class ActionEnterWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionEnter();
    }
}
