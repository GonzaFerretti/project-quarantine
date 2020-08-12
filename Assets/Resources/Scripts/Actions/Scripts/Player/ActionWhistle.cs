using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWhistle : ActionMakeNoise
{
    public override void Do(Model m)
    {
        base.Do(m);
        ModelPlayable mp = m as ModelPlayable;
        m.sm.Play(clip);
        mp.controller = mp.usualController;
        mp.rangeIndicator.gameObject.SetActive(false);
        mp.animator.SetTrigger("whistle");
        mp.animator.SetBool("isInWhistleMode", false);
    }
}
