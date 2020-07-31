using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStun : IAction
{
    float _knockStrength;

    public ActionStun(float knockStrength)
    {
        _knockStrength = knockStrength;
    }

    public virtual void Do(Model m)
    {        GameObject impactedObject = (m as FlingObject).impactedObject;
        if (impactedObject.GetComponentInParent<ModelPatrol>())
        {
            ModelPatrol patrol = impactedObject.GetComponentInParent<ModelPatrol>();
            patrol.controller = patrol.koController;
            Vector3 forceToApply = unifiedVectorWithoutY(m.GetComponent<Rigidbody>().velocity) * _knockStrength;
            patrol.ragdoll.KnockOut(forceToApply);
        }
        else if (impactedObject.GetComponent<ModelPoliceCar>())
        {
            ModelPoliceCar pcar = impactedObject.GetComponent<ModelPoliceCar>();
        }
    }
    
    Vector3 unifiedVectorWithoutY(Vector3 baseVector)
    {
        Vector3 vector = new Vector3(baseVector.x, 0, baseVector.z).normalized;
        return vector;
    }
}
