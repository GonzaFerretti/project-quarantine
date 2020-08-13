using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Firecracker")]
public class ActionFirecrackerWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionFirecracker();
        base.SetAction();
    }
}