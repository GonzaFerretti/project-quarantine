using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public IndoorSetter indoorSetter;
    public IndoorAttributes indoorAttributes;

    public IndoorAttributes GenerateIndoorAttributes()
    {
        IndoorAttributes newIndoors = ScriptableObject.CreateInstance("IndoorAttributes") as IndoorAttributes;
        newIndoors.storyAmount = Random.Range(1, 3);
        newIndoors.height = 2;
        newIndoors.layout = new IndoorUShape().SetParams();
        return newIndoors;
    }
}
