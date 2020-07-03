using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideCamMaskCheck : MonoBehaviour
{
    public List<Collider> hiddenObjects = new List<Collider>();
    public int buildingLayerId;
    public int maskedBuildingLayerId;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == buildingLayerId)
        {
            other.gameObject.layer = maskedBuildingLayerId;
            if (!hiddenObjects.Contains(other))
            {
                hiddenObjects.Add(other);
            }
        }
    }

    public void SwitchPosition()
    {
        foreach(Collider col in hiddenObjects)
        {
            col.gameObject.layer = buildingLayerId;
        }
        hiddenObjects = new List<Collider>();
    }
}
