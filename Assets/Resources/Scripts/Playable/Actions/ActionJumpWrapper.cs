using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Jump")]
public class ActionJumpWrapper : ActionWrapper
{
    public float jumpForce;

    public override void SetAction()
    {
        action = new ActionJump(jumpForce);
    }
}
