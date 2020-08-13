using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CurveDrawer : MonoBehaviour
{
    public float step = 1;
    public Vector3 speed;
    public GameObject prefab;
    public Vector3 startPos;
    public Vector3 endPos;
    private Vector3 lastSpeed;
    public List<GameObject> points = new List<GameObject>();
    
    public float autoOffTime;
    public float autoOffTimer = 0;
    public bool hasBeenDisabled = false;

    public void Draw()
    {
        foreach (GameObject point in points)
        {
            DestroyImmediate(point);
        }
        points = new List<GameObject>();
        float initialY = 0;
        float lastX = float.MaxValue;
        bool hasFinished = true;
        for (float i = 0; hasFinished; i += step)
        {
            float x = startPos.x + speed.x * (i);
            float y = startPos.y + speed.y * (i) +  (Physics.gravity.y * Mathf.Pow(i, 2))/2;
            float z = startPos.z + speed.z * (i);
            Vector3 currentPoint = new Vector3(x, y, z);
            GameObject newPoint = Instantiate(prefab, null);
            newPoint.transform.position = currentPoint;
            points.Add(newPoint);
            if (i == 0)
            {
                initialY = y;
            }
            else if (y < endPos.y)
            {
                hasFinished = false;
            }
            lastX = x;
            lastSpeed = speed;
        }
    }

    private void Update()
    {
        if (autoOffTimer < autoOffTime)
        {
            autoOffTimer += Time.deltaTime;
        }
        else if (!hasBeenDisabled)
        {
            Hide();
            hasBeenDisabled = true;
        }
    }

    public void UpdateDrawData(Vector3 _speed, Vector3 _startPos, Vector3 _end)
    {
        speed = _speed;
        startPos = _startPos;
        endPos = _end;
    }

    public void Clean()
    {
        foreach (GameObject point in points)
        {
            DestroyImmediate(point);
            points = new List<GameObject>();
        }
    }

    public void Hide()
    {
        Clean();
        autoOffTimer = 0;
        hasBeenDisabled = false;
    }
}
