using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Whistle")]
public class ActionWhistleWrapper : ActionWrapper
{
    public SoundClip whistleSound;
    public override void SetAction()
    {
        action = new ActionWhistle(whistleSound);
    }
}

