using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOPatrolManager : MonoBehaviour
{
    public List<ModelPatrol> KOPatrolList;

    public void AddPatrol(ModelPatrol m)
    {
        if (KOPatrolList == null) KOPatrolList = new List<ModelPatrol>();
        if(!KOPatrolList.Contains(m))KOPatrolList.Add(m);
    }

    public void RemovePatrol(ModelPatrol m)
    {
        if(KOPatrolList.Contains(m))KOPatrolList.Remove(m);
    }
}