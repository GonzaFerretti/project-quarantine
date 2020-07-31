using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWhistle : ActionMakeNoise
{
    public SoundClip _whistleSound;

    public ActionWhistle(SoundClip whistleSound)
    {
        _whistleSound = whistleSound;
    }

    public override void Do(Model m)
    {
        base.Do(m);
        ModelPlayable mp = m as ModelPlayable;
        m.sm.Play(_whistleSound);
        mp.controller = mp.usualController;
        mp.rangeIndicator.gameObject.SetActive(false);
        mp.animator.SetTrigger("whistle");
        mp.animator.SetBool("isInWhistleMode", false);
    }
}
