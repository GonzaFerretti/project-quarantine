using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Rotation")]
public class ActionRotationWrapper : ActionWrapper
{
    public float speed;
    public float maxDuration;
    public override void SetAction()
    {
        action = new ActionRotation(speed, maxDuration);
    }
}
