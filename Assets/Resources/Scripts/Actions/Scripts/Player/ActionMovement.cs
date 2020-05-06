using UnityEngine;
using System.Collections.Generic;

public class ActionMovement : IAction
{
    movementKeysDirection _direction;
    Dictionary<movementKeysDirection, Vector3> directionVectors = new Dictionary<movementKeysDirection, Vector3>();
    public ActionMovement(movementKeysDirection direction)
    {
        directionVectors.Add(movementKeysDirection.up, new Vector3(0.5f, 0, 0.5f));
        directionVectors.Add(movementKeysDirection.down, new Vector3(-0.5f, 0, -0.5f));
        directionVectors.Add(movementKeysDirection.right, new Vector3(0.5f, 0, -0.5f));
        directionVectors.Add(movementKeysDirection.left, new Vector3(-0.5f, 0, 0.5f));
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
        if (currentlyPressedAmount < 3)
        {
            float diagonalMultiplier = (currentlyPressedAmount > 1) ? Mathf.Sqrt(2) : 1;
            m.transform.position += directionVector.normalized * mc.currentSpeed * Time.deltaTime / diagonalMultiplier;
            //tentative
            m.transform.forward += directionVector.normalized;
        }
    }
}

public enum movementKeysDirection
{
    up = 0,
    down = 1,
    right = 2,
    left = 3,
}
