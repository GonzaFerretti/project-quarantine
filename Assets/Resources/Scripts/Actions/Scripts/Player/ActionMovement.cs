using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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

    public void Do(ModelChar m)
    {
        Vector3 directionVector = directionVectors[_direction];
        int currentlyPressedAmount = 0;
        //Debug.Log(m.movementKeys.Count);
        foreach(KeyCode key in m.movementKeys)
        {
            if (Input.GetKey(key))
            {
                currentlyPressedAmount++;
            }
        }
        float diagonalMultiplier = (currentlyPressedAmount > 1) ? Mathf.Sqrt(2) : 1;
        m.transform.position += directionVector.normalized * m.currentSpeed * Time.deltaTime * diagonalMultiplier;
        m.transform.forward += directionVector.normalized;
    }
}

public enum movementKeysDirection
{
    up = 0,
    down = 1,
    right = 2,
    left = 3,
}
