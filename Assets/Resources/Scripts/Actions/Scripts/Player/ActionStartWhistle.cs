using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStartWhistle : IAction
{
    public void Do(Model m)
    {
        ModelPlayable mp = m as ModelPlayable;
        mp.controller = mp.whistleController;
        mp.controller.SetController();
        mp.controller.myController.AssignModel(m);
        WhistleController wc = mp.controller as WhistleController;
        mp.whistleStrength = wc.minSize;
        wc.InitWhistle(mp.myAttributes.whistleStrength);
        mp.rangeIndicator.UpdateSize(wc.minSize * 2 / mp.transform.localScale.x);
        mp.rangeIndicator.gameObject.SetActive(true);
        mp.animator.SetBool("isInWhistleMode", true);
    }
}
