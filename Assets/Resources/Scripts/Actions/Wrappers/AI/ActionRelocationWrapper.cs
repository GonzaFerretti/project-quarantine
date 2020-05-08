using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Relocation")]
public class ActionRelocationWrapper : EnemyActionWrapper
{
    public Vector3 targetLocation;

    public override void SetAction()
    {
        action = new ActionRelocation(targetLocation);
    }

    public override void SetParams(PatrolNode node)
    {
        for (int i = 0; i < node.queuedAction.Length; i++)
        {
            if (node.queuedAction[i] == this)
            {
                node.queuedAction[i] = CreateInstance("ActionRelocationWrapper") as ActionWrapper;
                (node.queuedAction[i] as ActionRelocationWrapper).targetLocation = node.nextNode.transform.position;
                break;
            }
        }
    }
}
