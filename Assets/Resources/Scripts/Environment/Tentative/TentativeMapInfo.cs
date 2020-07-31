using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tentative/TentativeMapInfo")]
public class TentativeMapInfo : ScriptableObject
{
    public List<Item> items;
    public List<Vector3> positions;
    public List<Vector3> rotations;

    public List<Vector3> fencePositions;
    public List<Vector3> fenceRotations;
    public List<bool> fenceWasBroken;

    //public List<Vector3> scales;
    //public List<float> radiuses;
    //public List<Vector3> boxColliderSizes;

    public bool shouldCameraMove;
    public bool enteredBefore;    

    public Vector3 camPosition;
    public Vector3 camRotation;
    public float distance;

    public TentativeMapInfo Clone()
    {
        TentativeMapInfo newTentativeMapInfo = CreateInstance("TentativeMapInfo") as TentativeMapInfo;
        newTentativeMapInfo.name = name;
        newTentativeMapInfo.items = new List<Item>();
        newTentativeMapInfo.shouldCameraMove = shouldCameraMove;
        for (int i = 0; i < items.Count; i++)
        {
            newTentativeMapInfo.items.Add(items[i]);
        }

        newTentativeMapInfo.positions = new List<Vector3>();
        for (int i = 0; i < positions.Count; i++)
        {
            newTentativeMapInfo.positions.Add(positions[i]);
        }

        newTentativeMapInfo.rotations = new List<Vector3>();
        for (int i = 0; i < rotations.Count; i++)
        {
            newTentativeMapInfo.rotations.Add(rotations[i]);
        }

        newTentativeMapInfo.fencePositions = fencePositions;
        newTentativeMapInfo.fenceRotations = fenceRotations;
        newTentativeMapInfo.fenceWasBroken = fenceWasBroken;

        //newTentativeMapInfo.scales = new List<Vector3>();
        //for (int i = 0; i < scales.Count; i++)
        //{
        //    newTentativeMapInfo.scales.Add(scales[i]);
        //}

        //newTentativeMapInfo.radiuses = new List<float>();
        //for (int i = 0; i < radiuses.Count; i++)
        //{
        //    newTentativeMapInfo.radiuses.Add(radiuses[i]);
        //}

        //newTentativeMapInfo.boxColliderSizes = new List<Vector3>();
        //for (int i = 0; i < boxColliderSizes.Count; i++)
        //{
        //    newTentativeMapInfo.boxColliderSizes.Add(boxColliderSizes[i]);
        //}

        return newTentativeMapInfo;
    }
}