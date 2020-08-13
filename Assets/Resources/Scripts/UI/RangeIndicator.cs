using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public List<GameObject> currentCollidingObjects = new List<GameObject>();
    private int groundLayer;

    public void Start()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            if (!(currentCollidingObjects.Contains(other.gameObject)))
            {
            currentCollidingObjects.Add(other.gameObject);
            }
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            if (currentCollidingObjects.Contains(other.gameObject)) currentCollidingObjects.Remove(other.gameObject);
        }
    }

    public void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void UpdateSize(float size)
    {
        transform.localScale = new Vector3(size, size, 1);
    }
}