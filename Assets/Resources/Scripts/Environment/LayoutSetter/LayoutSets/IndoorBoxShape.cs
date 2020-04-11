using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorBoxShape : ILayout
{
    int _width;
    int _breadth;

    public void SetLayout(int height, IndoorSetter indoorSetter)
    {
        for (int i = 0- _width/2; i < _width/2; i++)
        {
            for (int j = 0 - _breadth/2; j < _breadth/2; j++)
            {
                GameObject newTile = MonoBehaviour.Instantiate(indoorSetter.floorTiles);
                newTile.transform.position = new Vector3(i,height,j);
            }
        }
    }

    public ILayout SetParams()
    {
        _width = Random.Range(3, 11) * 2;
        _breadth = Random.Range(3, 11) * 2;
        return this;
    }
}
