using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static Dictionary<string, int> currentResources = new Dictionary<string, int>();
    public static Dictionary<string, int> requiredResources = new Dictionary<string, int>();
    public static Dictionary<string, ResourceUI> Uielements = new Dictionary<string, ResourceUI>();

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

    public static void ResetResources()
    {
        currentResources = new Dictionary<string, int>();
        requiredResources = new Dictionary<string, int>();
        Uielements = new Dictionary<string, ResourceUI>();
    }

    public static void AddToResourceDict(string resourceName, int amount, ref Dictionary<string, int> resourceDict)
    {
        if (!resourceDict.ContainsKey(resourceName))
        {
            resourceDict.Add(resourceName, amount);  
        }
        else
        {
            resourceDict[resourceName] += amount;
        }

        if (!Uielements.ContainsKey(resourceName))
        {
            ResourceUI[] resourceUIs = FindObjectsOfType<ResourceUI>();
            foreach (ResourceUI resourceUi in resourceUIs)
            {
                if (resourceUi.Resource.resourceName == resourceName)
                {
                    Uielements.Add(resourceName, resourceUi);
                }
            }
        }

        if (Uielements.ContainsKey(resourceName))
        {
            int _amountRequired = 14;
            int _amountCurrent = 14;
            try { _amountRequired = requiredResources[resourceName]; }
            catch { _amountRequired = 0;}
            try { _amountCurrent = currentResources[resourceName]; }
            catch { _amountCurrent = 0; }
            Uielements[resourceName].UpdateUI(_amountCurrent, _amountRequired);
        }
    }

    public static void ReloadUI()
    {
        Uielements = new Dictionary<string, ResourceUI>();
        foreach (KeyValuePair<string, int> res in requiredResources)
        {
            if (!Uielements.ContainsKey(res.Key))
            {
                ResourceUI[] resourceUIs = FindObjectsOfType<ResourceUI>();
                foreach (ResourceUI resourceUi in resourceUIs)
                {
                    if (resourceUi.Resource.resourceName == res.Key)
                    {
                        Uielements.Add(res.Key, resourceUi);
                    }
                }
            }

            if (Uielements.ContainsKey(res.Key))
            {
                int _amountRequired;
                int _amountCurrent;
                try { _amountRequired = requiredResources[res.Key]; }
                catch { _amountRequired = 0; }
                try { _amountCurrent = currentResources[res.Key]; }
                catch { _amountCurrent = 0; }
                Uielements[res.Key].UpdateUI(_amountCurrent, _amountRequired);
            }
        }
        foreach (KeyValuePair<string, int> res in currentResources)
        {
            if (!Uielements.ContainsKey(res.Key))
            {
                ResourceUI[] resourceUIs = FindObjectsOfType<ResourceUI>();
                foreach (ResourceUI resourceUi in resourceUIs)
                {
                    if (resourceUi.Resource.resourceName == res.Key)
                    {
                        Uielements.Add(res.Key, resourceUi);
                    }
                }
            }

            if (Uielements.ContainsKey(res.Key))
            {
                int _amountRequired;
                int _amountCurrent;
                try { _amountRequired = requiredResources[res.Key]; }
                catch { _amountRequired = 0; }
                try { _amountCurrent = currentResources[res.Key]; }
                catch { _amountCurrent = 0; }
                Uielements[res.Key].UpdateUI(_amountCurrent, _amountRequired);
            }
        }
    }
}
