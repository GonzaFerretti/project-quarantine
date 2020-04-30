using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Fling")]
public class ActionFlingWrapper : ActionWrapper
{
    public override void SetAction()
    {
        action = new ActionFling();
    }
}
