using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static Dictionary<string, int> currentResources = new Dictionary<string, int>();
    public static Dictionary<string, int> requiredResources = new Dictionary<string, int>();
    public Resource foodResource;

    public static bool CheckResourceRequirements()
    {
        if (requiredResources.Count == 0)
        {
            return false;
        }
        bool hasMetRequirements = true;
        foreach (KeyValuePair<string, int> resource in requiredResources)
        {
            string resourceName = resource.Key;
            int amountReq = resource.Value;
            bool currentRequirementMet = currentResources.ContainsKey(resourceName) && currentResources[resourceName] == amountReq;
            hasMetRequirements = hasMetRequirements && currentRequirementMet;
        }
        return hasMetRequirements;
    }

    public static void AddToResourceDict(string resourceName, int amount, ref Dictionary<string, int> resourceDict)
    {
        if (!resourceDict.ContainsKey(resourceName)) resourceDict.Add(resourceName, amount);
        else resourceDict[resourceName] += amount;
    }

    public void Update()
    {
        Debug.Log(currentResources.Count);
        Debug.Log(currentResources["Food"]);
    }

    //public static void AddResource(Item item)
    //{
    //    int index = -1;
    //    for (int i = 0; i < currentResources.Count; i++)
    //    {
    //        if (currentResources[i].resourceName == item.resource.resourceName)
    //        {
    //            index = i;
    //            break;
    //        }
    //    }
    //    if (index != -1)
    //    {
    //        currentResources[index].currentAmount += item.amountPerResource;
    //        if (currentResources[index].resourceName == GameObject.FindObjectOfType<ResourceManager>().foodResource.resourceName)
    //        {
    //            FindObjectOfType<FoodBar>().UpdateFoodBar(currentResources[index].currentAmount);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Resource not found");
    //    }
    //}
}
