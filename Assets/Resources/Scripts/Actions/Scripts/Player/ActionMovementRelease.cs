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
            ModelPlayable mp = m as ModelPlayable;
            int currentlyPressedAmount = 0;
            foreach (KeyValuePair<KeyCode, movementKeysDirection> key in mp.movementKeys)
            {
                if (Input.GetKey(key.Key))
                {
                    currentlyPressedAmount++;
                }
            }
            if (currentlyPressedAmount < 1)
            {
                mp.animator.SetBool("isRunning", false);
                mp.animator.SetTrigger("idleVariation");
            }
        }
    }

}