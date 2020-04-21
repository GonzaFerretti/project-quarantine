using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorBoxShape : ILayout
{
    public int width;
    public int breadth;
    Vector3 _placement;
    int _minWidth;
    int _maxWidth;
    int _minBreadth;
    int _maxBreadth;

    public IndoorBoxShape(int minWidth, int maxWidth, int minBreadth, int maxBreadth)
    {
        _minWidth = minWidth;
        _maxWidth = maxWidth;
        _minBreadth = minBreadth;
        _maxBreadth = maxBreadth;
    }

    public void SetLayout(int height, MapSetter mapSetter)
    {
        for (int i = 0 - width / 2; i < width / 2; i++)
        {
            for (int j = 0 - breadth / 2; j < breadth / 2; j++)
            {
                mapSetter.CreateTiles(new Vector3(i, height, j) + _placement);
            }
        }
    }

    public ILayout SetParams()
    {
        width = Random.Range(_minWidth, _maxWidth) * 2;
        breadth = Random.Range(_minBreadth, _maxBreadth) * 2;
        return this;
    }

    public int ReturnWidth()
    {
        return width;
    }

    public int ReturnBreadth()
    {
        return breadth;
    }

    public IndoorBoxShape Attributes(Vector3 placement)
    {
        _placement = placement;
        return this;
    }
}
