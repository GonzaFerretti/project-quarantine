using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Fling")]
public class ActionFlingWrapper : ActionWrapper
{
    public float upwardStrength;
    public override void SetAction()
    {
        action = new ActionFling(upwardStrength);
    }
}
