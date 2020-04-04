using UnityEngine;

public class ActionGrab : IAction
{
    public void Do(ModelChar m)
    {
        Debug.Log("Grab");
    }
}
