using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<Resource> resources;

    private void Start()
    {
        CloneResources();
    }

    void CloneResources()
    {
        for (int i = 0; i < resources.Count; i++)
        {
            resources[i] = resources[i].Clone();
        }
    }
}
