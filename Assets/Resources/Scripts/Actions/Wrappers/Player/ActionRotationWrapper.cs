using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Rotation")]
public class ActionRotationWrapper : ActionWrapper
{
    public Vector3 quaternion;
    public float speed;

    public override void SetAction()
    {
        action = new ActionRotation(speed, quaternion);
    }
}
