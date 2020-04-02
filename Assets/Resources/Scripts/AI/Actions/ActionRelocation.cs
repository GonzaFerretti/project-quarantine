using UnityEngine;
using UnityEngine.AI;

public class ActionRelocation : IAction
{
    Vector3 _targetLoc;

    public ActionRelocation(Vector3 targetLocation)
    {
        _targetLoc = targetLocation;
    }

    public void Do(ModelChar m)
    {
        NavMeshAgent agent = m.GetComponent<NavMeshAgent>();
        agent.speed = m.currentSpeed;
        agent.SetDestination(_targetLoc);
    }
}
