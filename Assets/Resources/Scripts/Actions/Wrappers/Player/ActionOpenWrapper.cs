using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Open")]
public class ActionOpenWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionOpen();
    }
}
