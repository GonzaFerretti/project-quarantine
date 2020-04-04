using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Rotation")]
public class ActionRotationWrapper : EnemyActionWrapper
{
    public float speed;
    public float maxDuration;
    public override void SetAction()
    {
        action = new ActionRotation(speed, maxDuration);
    }

    public override void SetParams(PatrolNode node)
    {
        (action as ActionRotation).SetNode(currentNode);
    }
}
