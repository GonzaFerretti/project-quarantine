using UnityEngine;

public class ActionDuck : IAction
{
    public ActionDuck()
    {
    }

    public void Do(ModelChar m)
    {
        m.isDucking = true;
    }
}
