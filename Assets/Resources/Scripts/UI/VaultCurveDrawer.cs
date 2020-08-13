using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultCurveDrawer : MonoBehaviour
{
    float A;
    float B;
    List<GameObject> points = new List<GameObject>();
    [SerializeField]float step;
    [SerializeField] GameObject prefab;
    public float autoOffTime;
    public float autoOffTimer = 0;
    public bool hasBeenDisabled = false;

    public void Update()
    {
        if (autoOffTimer<autoOffTime)
        {
            autoOffTimer += Time.deltaTime;
        }
        else if (!hasBeenDisabled) {
            Hide();
            hasBeenDisabled = true;
        }
    }

    public Quaternion GetCurrentPlayerQuaternion(Transform transform)
    {
        return Quaternion.Euler(transform.eulerAngles + Vector3.up * -90f);
    }

    public void GetPointToPointParabola(float x2, float y3)
    {
        // Solving for a parabola that passes through 3 points.
        // With the generic notation for a parabola: ax + bx^2 + c = y, i found the following:
        //double denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
        //double A = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
        //double B = (x3 * x3 * (y1 - y2) + x2 * x2 * (y3 - y1) + x1 * x1 * (y2 - y3)) / denom;
        //double C = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;
        //However this can simplified because in our case,x1 = 0, y1 = 0, y2 = 0, and x3= (x1+x2)/2, resulting in the following:
        float denom = (x2) * (x2 / 2) * (x2 / 2);
        A = (y3 * (-x2)) / denom;
        B = (x2 * x2 * (y3)) / denom;
    }

    public Vector3 GetCurrentParabolaPoint(float x)
    {
        float y = A * Mathf.Pow(x,2) + B * x;
        return new Vector3(x, y, 0);
    }

    public void UpdateLine(Transform playerTransform, Vector3 originPoint, Vector3 endPoint, float height)
    {
        float distance = Vector3.Distance(originPoint, endPoint);
        GetPointToPointParabola(distance, height);
        Draw(playerTransform, originPoint, endPoint);
    }

    public void Draw(Transform playerTransform, Vector3 originPoint, Vector3 endPoint)
    {
        Quaternion playerDirection = GetCurrentPlayerQuaternion(playerTransform);
        foreach (GameObject point in points)
        {
            DestroyImmediate(point);
        }
        points = new List<GameObject>();
        float distance = Vector3.Distance(originPoint, endPoint);
        for (float i = 0; i <= distance; i += step)
        {
            Vector3 currentPoint = playerDirection * ( GetCurrentParabolaPoint(i) ) + originPoint;
            GameObject newPoint = Instantiate(prefab, null);
            newPoint.transform.position = currentPoint;
            points.Add(newPoint);
        }
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
/*
    Vector3 vectorWithoutY(Vector3 vector)
    {
        return new Vector3(vector.x,vector.x)
    }*/
}
