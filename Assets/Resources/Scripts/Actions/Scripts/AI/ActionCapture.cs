using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCapture : IAction
{
    ModelPatrol _mp;
    LayerMask _layerMask;

    public ActionCapture(LayerMask l)
    {
        _layerMask = l;
    }

    public void Do(Model m)
    {
        if (!(m is ModelPatrol)) return;
        if (_mp == null) _mp = m as ModelPatrol;
        RaycastHit hit;
        Vector3 _dir = (_mp.target.transform.position - m.transform.position).normalized * _mp.meleeDistance;
        Physics.Raycast(_mp.transform.position, _dir, out hit);

        if (hit.collider && hit.collider.GetComponent<ModelPlayable>())
        {
            EventManager.TriggerEvent("Loss");
        }
    }
}