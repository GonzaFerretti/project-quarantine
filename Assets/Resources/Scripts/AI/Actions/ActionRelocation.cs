using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRelocation : IAction
{
    Vector3 _targetLoc;

    public ActionRelocation(Vector3 targetLocation)
    {
        _targetLoc = targetLocation;
    }

    public void Do(ModelChar m)
    {
        m.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_targetLoc);
    }
}
