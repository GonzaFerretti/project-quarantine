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
            if (queuedAction[i] is ActionMovementWrapper && (queuedAction[i] as ActionMovementWrapper).direction == Vector3.zero)
            {
                queuedAction[i] = ScriptableObject.CreateInstance("ActionMovementWrapper") as ActionWrapper;
                (queuedAction[i] as ActionMovementWrapper).direction = (nextNode.transform.position - transform.position).normalized;
            }

                queuedAction[i].SetAction();

            if (queuedAction[i] is ActionRotationWrapper)
            {
                ((queuedAction[i] as ActionRotationWrapper).action as ActionRotation).SetNode(this);
            }

        }
    }

    private void OnDrawGizmos()
    {
        if(nextNode != null)
        Gizmos.DrawLine(transform.position, nextNode.transform.position);
    }
}
