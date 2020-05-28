using UnityEngine;
using System.Collections.Generic;

public class ActionMovement : IAction
{
    movementKeysDirection _direction;
    public static Dictionary<movementKeysDirection, Vector3> directionVectors = new Dictionary<movementKeysDirection, Vector3>();
    public static bool hasSetDict;
    public ActionMovement(movementKeysDirection direction)
    {
        if (!hasSetDict)
        {
        hasSetDict = true;
        directionVectors.Add(movementKeysDirection.up, new Vector3(0.5f, 0, 0.5f));
        directionVectors.Add(movementKeysDirection.down, new Vector3(-0.5f, 0, -0.5f));
        directionVectors.Add(movementKeysDirection.right, new Vector3(0.5f, 0, -0.5f));
        directionVectors.Add(movementKeysDirection.left, new Vector3(-0.5f, 0, 0.5f));
        }
        _direction = direction;
    }

    public void Do(Model m)
    {
        Vector3 directionVector = directionVectors[_direction];
        int currentlyPressedAmount = 0;
        ModelChar mc = m as ModelChar;

        foreach (KeyCode key in (mc.controller as PlayerController).movementKeys)
        {
            if (Input.GetKey(key))
            {
                currentlyPressedAmount++;
            }
        }
        if (currentlyPressedAmount < 3 && !((mc as ModelHumanoid).isVaulting))
        {
            float diagonalMultiplier = (currentlyPressedAmount > 1) ? Mathf.Sqrt(2) : 1;
            mc.animator.ResetTrigger("idleVariation");
            m.transform.position += directionVector.normalized * mc.currentSpeed * Time.deltaTime / diagonalMultiplier;
            mc.animator.SetBool("isRunning", true);
            //tentative
            m.transform.forward += directionVector.normalized;
        }
    }

    public static void resetDirections()
    {
        directionVectors[movementKeysDirection.up] = new Vector3(0.5f, 0, 0.5f);
        directionVectors[movementKeysDirection.down] = new Vector3(-0.5f, 0, -0.5f);
        directionVectors[movementKeysDirection.right] =  new Vector3(0.5f, 0, -0.5f);
        directionVectors[movementKeysDirection.left] =  new Vector3(-0.5f, 0, 0.5f);
    }

    public static void modifyDirections(Vector3 up, Vector3 left, Vector3 down, Vector3 right)
    {
        directionVectors[movementKeysDirection.up] = up;
        directionVectors[movementKeysDirection.left] = left;
        directionVectors[movementKeysDirection.down] = down;
        directionVectors[movementKeysDirection.right] = right;
    }
}

public enum movementKeysDirection
{
    up = 0,
    down = 1,
    right = 2,
    left = 3,
}
