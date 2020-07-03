using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
        Vector3 _dir = ((_mp.target.transform.position + HeightReturner(_mp.target.bodyHeight)) - (m.transform.position + HeightReturner(_mp.headHeight))).normalized;
        Physics.Raycast(_mp.transform.position + HeightReturner(_mp.headHeight), _dir, out hit, _mp.meleeDistance, _layerMask);

        if (hit.collider)
        {
            if (hit.collider.GetComponent<ModelPlayable>())
            {
                EventManager.TriggerEvent("Loss");
            }
        }
    }

    Vector3 HeightReturner(float f)
    {
        Vector3 newHeight = new Vector3(0, f, 0);
        return newHeight;
    }
}