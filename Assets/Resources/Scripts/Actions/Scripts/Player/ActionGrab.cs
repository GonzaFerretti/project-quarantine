using UnityEngine;

public class ActionGrab : IAction
{
    public void Do(ModelChar m)
    {
        if (m is ModelPlayable)
        {
        }
    }
}
