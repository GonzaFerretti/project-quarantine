using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorUShape : ILayout
{
    int[] westWing;

    int[] center;

    int[] eastWing;


    public void SetLayout(int height, IndoorSetter indoorSetter)
    {
        GameObject firstTile = null;
        GameObject lasTile = null;

        for (int i = 0 - center[0] / 2; i < center[0] / 2; i++)
        {
            for (int j = 0 - center[1] / 2; j < center[1] / 2; j++)
            {
                GameObject newTile = MonoBehaviour.Instantiate(indoorSetter.floorTiles);
                if (i == 0 - center[0] / 2 && j == center[1] / 2 - 1)
                {
                    firstTile = newTile;
                }
                if (i == (center[0] / 2 - 1) && j == (center[1] / 2 - 1))
                {
                    lasTile = newTile;
                }
                newTile.name = "Center" + i + "" + j;
                newTile.transform.position = new Vector3(i, height, j);

                if (lasTile != null)
                    lasTile.name = "lasTile";
                if (firstTile != null)
                    firstTile.name = "firsTile";
            }
        }

        for (int i = 0 - westWing[0]; i < westWing[0] / 2; i++)
        {
            for (int j = westWing[1] - 1; j >= 0 - westWing[1] / 2; j--)
            {
                GameObject newTile = MonoBehaviour.Instantiate(indoorSetter.floorTiles);
                newTile.transform.position = new Vector3(firstTile.transform.position.x + westWing[0] + i, height, firstTile.transform.position.z + (center[1] / 2) - j);
            }
        }

        for (int i = eastWing[0] / 2 - 1; i >= 0 - eastWing[0]; i--)
        {
            for (int j = eastWing[1] - 1; j >= 0 - eastWing[1] / 2; j--)
            {
                GameObject newTile = MonoBehaviour.Instantiate(indoorSetter.floorTiles);
                newTile.transform.position = new Vector3(lasTile.transform.position.x - eastWing[0] - i, height, firstTile.transform.position.z + (center[1] / 2) - j);
            }
        }
    }

    public ILayout SetParams()
    {
        center = new int[2];
        center[0] = Random.Range(3, 10) * 2;
        center[1] = Random.Range(3, 5) * 2;
        westWing = new int[2];
        westWing[0] = Random.Range(2, 3) * 2;
        westWing[1] = Random.Range(2, 4) * 2;
        eastWing = new int[2];
        eastWing[0] = Random.Range(2, 3) * 2;
        eastWing[1] = Random.Range(2, 4) * 2;
        return this;
    }
}
