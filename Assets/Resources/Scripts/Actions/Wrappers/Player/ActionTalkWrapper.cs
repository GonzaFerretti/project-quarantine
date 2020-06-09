using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Talk")]
public class ActionTalkWrapper : ActionWrapper
{
    public float dist;

    public override void SetAction()
    {
        action = new ActionTalk(dist);
    }
}
