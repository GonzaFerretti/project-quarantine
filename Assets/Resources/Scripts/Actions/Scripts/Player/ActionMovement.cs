using UnityEngine;
using System.Collections.Generic;

public class ActionMovement : IAction
{
    public movementKeysDirection _direction;
    public static Dictionary<movementKeysDirection, Vector3> directionVectors = new Dictionary<movementKeysDirection, Vector3>();
    public ActionMovement(movementKeysDirection direction)
    {
        _direction = direction;
    }

    public void Do(Model m)
    {
        Vector3 directionVector = directionVectors[_direction];
        ModelPlayable mp = m as ModelPlayable;
        Dictionary<movementKeysDirection, bool> currentlyPressedKeys = new Dictionary<movementKeysDirection, bool>();
        foreach (KeyValuePair<KeyCode, movementKeysDirection> key in mp.movementKeys)
        {
            currentlyPressedKeys[key.Value] = Input.GetKey(key.Key);
        }
        bool bothHorizontal = (currentlyPressedKeys[movementKeysDirection.left] && currentlyPressedKeys[movementKeysDirection.right]);
        bool bothVertical = (currentlyPressedKeys[movementKeysDirection.up] && currentlyPressedKeys[movementKeysDirection.down]);
        bool anyHorizontal = (currentlyPressedKeys[movementKeysDirection.left] || currentlyPressedKeys[movementKeysDirection.right]);
        bool anyVertical = (currentlyPressedKeys[movementKeysDirection.up] || currentlyPressedKeys[movementKeysDirection.down]);
        bool isCurrentInputVertical = _direction == movementKeysDirection.down || _direction == movementKeysDirection.up;
        bool isCurrentInputHorizontal = _direction == movementKeysDirection.left || _direction == movementKeysDirection.right;
        bool isPressingAll = bothVertical && bothHorizontal;

        bool canMoveVertical = (bothHorizontal && isCurrentInputVertical && !bothVertical) || (!bothVertical && isCurrentInputVertical);
        bool canMoveHorizontal = (bothVertical && isCurrentInputHorizontal && !bothHorizontal) || (!bothHorizontal && isCurrentInputHorizontal);
        bool isDiagonal = (isCurrentInputHorizontal && anyVertical) || isCurrentInputVertical && anyHorizontal;

        if ((canMoveHorizontal || canMoveVertical) && !((mp as ModelHumanoid).isVaulting) && !isPressingAll)
        {
            float diagonalMultiplier = (isDiagonal && !(bothHorizontal || bothVertical)) ? Mathf.Sqrt(2) : 1;
            mp.animator.ResetTrigger("idleVariation");
            m.transform.position += directionVector.normalized * mp.currentSpeed * Time.deltaTime / diagonalMultiplier;
            mp.animator.SetBool("isRunning", true);
            //tentative
            m.transform.forward += directionVector.normalized;
        }
        else if ((!isDiagonal && (bothHorizontal || bothVertical)) || isPressingAll) { mp.animator.SetBool("isRunning", false); }
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
