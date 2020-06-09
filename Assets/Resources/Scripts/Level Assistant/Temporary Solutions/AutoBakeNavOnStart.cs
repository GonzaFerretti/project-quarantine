using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoBakeNavOnStart : MonoBehaviour
{
    NavMeshSurface navMesh;
    private void Start()
    {
        try { 
        GetComponent<NavMeshSurface>().BuildNavMesh();
            Debug.Log("NavMesh built succesfully");
        }
        catch
        {
            Debug.LogError("Couldn't build NavMesh. Enemies won't be able to navigate.");
        }
    }
}
