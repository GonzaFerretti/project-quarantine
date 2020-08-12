using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStun : BaseAction
{
    float _knockStrength;
    ActionWrapper _actionToAdd;

    public ActionStun(float knockStrength, ActionWrapper action)
    {
        _knockStrength = knockStrength;
        _actionToAdd = action;
    }

    public override void Do(Model m)
    {        GameObject impactedObject = (m as FlingObject).impactedObject;
        if (impactedObject.GetComponentInParent<ModelPatrol>())
        {
            ModelPatrol patrol = impactedObject.GetComponentInParent<ModelPatrol>();
            patrol.controller = patrol.koController;
            Vector3 forceToApply = unifiedVectorWithoutY(m.GetComponent<Rigidbody>().velocity) * _knockStrength;
            patrol.ragdoll.KnockOut(forceToApply);
            patrol.GetComponent<InteractableObject>().requiredAction = _actionToAdd;
            (m as FlingObject).hasMissed = false;
            (m as FlingObject).sm.Play(clip);
        }
    }
    
    Vector3 unifiedVectorWithoutY(Vector3 baseVector)
    {
        Vector3 vector = new Vector3(baseVector.x, 0, baseVector.z).normalized;
        return vector;
    }
}
