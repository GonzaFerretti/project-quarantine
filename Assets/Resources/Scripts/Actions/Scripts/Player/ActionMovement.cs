﻿using UnityEngine;
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
        m.goingDirections[(int)_direction] = true;
        bool isPressingMultipleDirections = m.goingDirections.Count(c => c) >= 2;
        Debug.Log(isPressingMultipleDirections);
        m.transform.position += directionVector.normalized * m.currentSpeed * Time.deltaTime;
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
