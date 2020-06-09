#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
[ExecuteInEditMode]
public class AutoFillerFloor : MonoBehaviour
{
    [Header("Parameters")]
    public bool shouldAutoFill;
    public float borderWidth;
    public float floorHeight;
    public float fenceWidth;
    public float fenceHeight;
    public bool shouldAddFences;
    public GameObject fenceSample;

    [Header("Border information")]
    public float minX = 0;
    public float maxX = 0;
    public float minZ = 0, maxZ = 0;

    public GameObject minXBorder;
    public GameObject maxXBorder;
    public GameObject maxZBorder;
    public GameObject minZBorder;

    public void EnsureGameObjectExists(ref GameObject gameObject)
    {
        gameObject = (gameObject == null) ? Instantiate(fenceSample) : gameObject;
    }

    public void Update()
    {
        if (shouldAutoFill && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            maxX = float.MinValue;
            minX = float.MaxValue;
            minZ = float.MaxValue;
            maxZ = float.MinValue;
            Collider[] colliders = FindObjectsOfType<Collider>();
            for (int i = 0; i<colliders.Length;i++)
            {
                Collider col = colliders[i];
                if (col.transform != transform && col.tag != "borderFence")
                {
                    Vector3 mins = col.bounds.min;
                    Vector3 maxs = col.bounds.max;
                    minX = (mins.x < minX) ? mins.x : minX;
                    minZ = (mins.z < minZ) ? mins.z : minZ;

                    maxX = (maxs.x > maxX) ? maxs.x : maxX;
                    maxZ = (maxs.z > maxZ) ? maxs.z : maxZ;
                }
            }
            float distanceX = maxX - minX + borderWidth*2;
            float distanceZ = maxZ - minZ + borderWidth*2;
            float centerX = (maxX + minX) / 2;
            float centerZ = (maxZ + minZ) / 2;
            Vector3 center = new Vector3(centerX, floorHeight, centerZ);
            transform.localScale = new Vector3(distanceX, 1, distanceZ);
            transform.position = center;
            if (shouldAddFences)
            {
                EnsureGameObjectExists(ref minXBorder);
                EnsureGameObjectExists(ref maxXBorder);
                EnsureGameObjectExists(ref maxZBorder);
                EnsureGameObjectExists(ref minZBorder);
                float clampedFenceWidth = Mathf.Clamp(fenceWidth,0, borderWidth);
                float fenceCenter = clampedFenceWidth / 2;

                Vector3 minXCenter = new Vector3(minX - borderWidth + fenceCenter, fenceHeight / 2 + floorHeight, centerZ);
                Vector3 maxXCenter = new Vector3(maxX + borderWidth - fenceCenter, fenceHeight / 2 + floorHeight, centerZ);
                Vector3 minZCenter = new Vector3(centerX, fenceHeight/ 2 + floorHeight, minZ - borderWidth + fenceCenter);
                Vector3 maxZCenter = new Vector3(centerX, fenceHeight / 2 + floorHeight, maxZ + borderWidth - fenceCenter);

                minXBorder.transform.position = minXCenter;
                maxXBorder.transform.position = maxXCenter;
                minZBorder.transform.position = minZCenter;
                maxZBorder.transform.position = maxZCenter;

                minXBorder.transform.localScale = new Vector3(clampedFenceWidth, fenceHeight, distanceZ);
                maxXBorder.transform.localScale = minXBorder.transform.localScale;
                minZBorder.transform.localScale = new Vector3(distanceX, fenceHeight, clampedFenceWidth);
                maxZBorder.transform.localScale = minZBorder.transform.localScale;
            }
        }
    }
}
#endif