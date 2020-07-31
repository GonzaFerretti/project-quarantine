using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetInitialData(Vector3 pos, float size)
    {
        UpdatePosition(pos);
        UpdateSize(size);
    }

    public void UpdateSize(float size)
    {
        transform.localScale = new Vector3(size, size, 1);
    }
}