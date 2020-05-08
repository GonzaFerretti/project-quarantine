using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMovementRelease : IAction
{
    public ActionMovementRelease()
    {
    }

    public void Do(Model m)
    {
        if (m is ModelChar)
        {
            ModelChar mc = m as ModelChar;
            int currentlyPressedAmount = 0;
            foreach (KeyCode key in (mc.controller as PlayerController).movementKeys)
            {
                if (Input.GetKey(key))
                {
                    currentlyPressedAmount++;
                }
            }
            if (currentlyPressedAmount < 1)
            {
                mc.animator.SetBool("isRunning", false);
                mc.animator.SetTrigger("idleVariation");
            }
        }
    }

}