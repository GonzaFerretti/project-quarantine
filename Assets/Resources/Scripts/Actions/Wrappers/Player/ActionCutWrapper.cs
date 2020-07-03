using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Cut")]
public class ActionCutWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionCut();
    }
}