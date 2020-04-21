using UnityEngine;

public class ActionGrab : IAction
{
    public void Do(ModelChar m)
    {
        if (m.nearbyObject != null && m is ModelPlayable)
        {
            Vector3 baseDirection = (m.nearbyObject.transform.position - m.transform.position).normalized;
            Vector3 finalDirection = new Vector3(baseDirection.x, 0, baseDirection.z);
            Debug.Log("lo agarré");
            m.transform.forward = finalDirection;
        }
    }
}
