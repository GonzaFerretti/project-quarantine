using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Talk")]
public class ActionTalkWrapper : ActionBaseInteractWrapper
{
    public override void SetAction()
    {
        action = new ActionTalk(interactionDistance);
    }
}
