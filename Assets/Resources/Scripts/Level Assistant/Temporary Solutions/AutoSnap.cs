#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class AutoSnap : MonoBehaviour
{
    public bool shouldSnap;
    public Vector3 snapDistance;
    public Vector3 offset = new Vector3(0, 0, 0);

    private bool lastSnapStatus = false;
    private Vector3 lastPosition;
    public void Start()
    {
        lastPosition = transform.position;
    }

    private void checkSnapHotKey()
    {
        Event current = Event.current;
        if (current.type != EventType.KeyDown)
            return;
        //Debug.Log("funciona");
    }

    public void Update()
    {
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (!lastSnapStatus && shouldSnap)
            {
                RecalculateSnapDistance();
                RecalculateOffset();
            }
            if (shouldSnap)
            {
                if (lastPosition != transform.position)
                {
                    Snap();
                }
            }
            lastSnapStatus = shouldSnap;
        }
    }

    public float RoundToNearestMultiple(float value, float multiple)
    {
        if (multiple == 0)
            return 0;
        float nearestMultiple = Mathf.Round(value / multiple) * multiple;
        return nearestMultiple;
    }

    private void Snap()
    {
        float x, y, z;
        x = RoundToNearestMultiple(transform.position.x, snapDistance.x) + offset.x;
        y = RoundToNearestMultiple(transform.position.y, snapDistance.y) + offset.y;
        z = RoundToNearestMultiple(transform.position.z, snapDistance.z) + offset.z;
        transform.position = new Vector3(x, y, z);
        lastPosition = transform.position;
    }
    private void RecalculateSnapDistance()
    {
        float maxX = float.MinValue;
        float minX = float.MaxValue;
        float minZ = float.MaxValue;
        float maxZ = float.MinValue;
        float maxY = float.MinValue;
        float minY = float.MaxValue;
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider col = colliders[i];
            Vector3 mins = col.bounds.min;
            Vector3 maxs = col.bounds.max;
            minX = (mins.x < minX) ? mins.x : minX;
            minY = (mins.y < minY) ? mins.y : maxY;
            minZ = (mins.z < minZ) ? mins.z : minZ;
            
            maxX = (maxs.x > maxX) ? maxs.x : maxX;
            maxY = (maxs.y > maxY) ? maxs.y : maxY;
            maxZ = (maxs.z > maxZ) ? maxs.z : maxZ;
        }
        float distanceX = Math.Abs(maxX - minX);
        float distanceY = Math.Abs(maxY - minY);
        float distanceZ = Math.Abs(maxZ - minZ);
        snapDistance = new Vector3(distanceX, distanceY, distanceZ);
    }

    private void RecalculateOffset()
    {
        float x, y, z;
        x = transform.position.x - RoundToNearestMultiple(transform.position.x, snapDistance.x);
        y = transform.position.y - RoundToNearestMultiple(transform.position.y, snapDistance.y);
        z = transform.position.z - RoundToNearestMultiple(transform.position.z, snapDistance.z);
        offset = new Vector3(x, y, z);
    }
}
#endif
