using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void UpdatePosition(Vector3 pos, float size)
    {
        transform.position = pos;
        transform.localScale = new Vector3(size, size, 1);
    }
}
