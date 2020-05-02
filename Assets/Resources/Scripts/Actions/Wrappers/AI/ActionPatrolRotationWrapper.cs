using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Patrol Rotation")]
public class ActionPatrolRotationWrapper : EnemyActionWrapper
{
    public float speed;
    public float maxDuration;
    public Vector3 quaternion;

    public override void SetAction()
    {
        action = new ActionPatrolRotation(speed, maxDuration, quaternion);
    }

    public override void SetParams(PatrolNode node)
    {
        (action as ActionPatrolRotation).SetNode(currentNode);
    }
}
