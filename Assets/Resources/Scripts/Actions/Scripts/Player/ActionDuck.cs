using UnityEngine;

public class ActionDuck : IAction
{
    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        mh.isDucking = true;
    }
}
