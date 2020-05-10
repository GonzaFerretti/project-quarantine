using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDuckRelease : IAction
{
    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        mh.isDucking = false;
        mh.animator.SetBool("isCrawling", false);
    }
}
