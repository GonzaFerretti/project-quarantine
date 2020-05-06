using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Enter")]
public class ActionEnterWrapper : ActionWrapper
{
    public float interactionDistance;

    public override void SetAction()
    {
        action = new ActionEnter(interactionDistance);
    }
}
