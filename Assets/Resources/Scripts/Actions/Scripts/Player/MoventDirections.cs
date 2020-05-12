using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Controller/Action/Movement Direction")]
public class movementVector : ScriptableObject
{
    public movementKeysDirection keysDirection;
    public Vector3 directionVector;
}
