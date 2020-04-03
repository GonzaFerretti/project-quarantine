using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : MonoBehaviour
{
    public PatrolNode nextNode;
    public ActionWrapper[] queuedAction;
    public int currentAction;

    public void ActionChecker()
    {
        for (int i = 0; i < queuedAction.Length; i++)
        {
            if (queuedAction[i] is EnemyActionWrapper)
            {
                (queuedAction[i] as EnemyActionWrapper).SetParams(this);
            }
            queuedAction[i].SetAction();
        }
    }

    private void OnDrawGizmos()
    {
        if (nextNode != null)
            Gizmos.DrawLine(transform.position, nextNode.transform.position);
    }
}
