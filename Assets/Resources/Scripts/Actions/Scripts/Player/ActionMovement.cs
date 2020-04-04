using UnityEngine;

public class ActionMovement : IAction
{
    Vector3 _direction;
    public ActionMovement(Vector3 direction)
    {
        _direction = direction;
    }

    public void Do(ModelChar m)
    {
        if (m.vaultState != ModelChar.vaultStates.isVaulting)
        {
            m.transform.position += _direction.normalized * m.currentSpeed * Time.deltaTime;
            m.transform.forward += _direction.normalized;
        }

    }
}
