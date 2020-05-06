using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tentative/TentativeMapInfo")]
public class TentativeMapInfo : ScriptableObject
{
    public List<Item> items;
    public List<Vector3> positions;

    public TentativeMapInfo Clone()
    {
        TentativeMapInfo newTentativeMapInfo = CreateInstance("TentativeMapInfo") as TentativeMapInfo;
        newTentativeMapInfo.items = new List<Item>();
        for (int i = 0; i < items.Count; i++)
        {
            newTentativeMapInfo.items.Add(items[i]);
        }

        newTentativeMapInfo.positions = new List<Vector3>();
        for (int i = 0; i < positions.Count; i++)
        {
            newTentativeMapInfo.positions.Add(positions[i]);
        }

        return newTentativeMapInfo;
    }

}
