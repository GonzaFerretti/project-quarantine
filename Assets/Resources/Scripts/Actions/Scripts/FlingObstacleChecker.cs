using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingObstacleChecker : MonoBehaviour
{
    [SerializeField] List<GameObject> currentlyCloseObstacles = new List<GameObject>();
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (!(other.gameObject.layer == LayerMask.NameToLayer("Ground")) && !currentlyCloseObstacles.Contains(other.gameObject))
        { 
            currentlyCloseObstacles.Add(other.gameObject);
        }
    }

    public void Init()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        col.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentlyCloseObstacles.Contains(other.gameObject))
        {
            currentlyCloseObstacles.Remove(other.gameObject);
        }
    }

    public List<GameObject> GetCloseObstacles()
    {
        return currentlyCloseObstacles;
    }
}
