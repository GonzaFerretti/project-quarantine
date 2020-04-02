using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Action/Relocation")]
public class ActionRelocationWrapper : ActionWrapper
{
    public Vector3 targetLocation;

    public override void SetAction()
    {
        action = new ActionRelocation(targetLocation);
    }
}
