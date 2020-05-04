using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerType : ScriptableObject
{
    public abstract bool CheckTrigger(KeyCode key);
}
