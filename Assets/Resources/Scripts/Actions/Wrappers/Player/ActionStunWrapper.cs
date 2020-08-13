using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Stun")]
public class ActionStunWrapper : ActionWrapper
{
    public float knockStrength;
    public ActionWrapper actionToAdd;
    public override void SetAction()
    {
        action = new ActionStun(knockStrength, actionToAdd);
        base.SetAction();
    }
}
