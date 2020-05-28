using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Resource")]
public class Resource : ScriptableObject
{
    public string resourceName;
    public float currentAmount;
    public List<float> amounts;

    public Resource Clone()
    {
        Resource clone = CreateInstance("Resource") as Resource;
        clone.resourceName = resourceName;
        clone.amounts = amounts;
        return clone;
    }
}