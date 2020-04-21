using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorModuleLayout : ILayout
{
    int _roomAmount;

    public IndoorModuleLayout()
    {

    }

    public int ReturnBreadth()
    {
        throw new System.NotImplementedException();
    }

    public int ReturnWidth()
    {
        throw new System.NotImplementedException();
    }

    public void SetLayout(int height, MapSetter mapSetter)
    {
        ILayout center = new IndoorBoxShape(6, 6, 6, 6).Attributes(Vector3.zero).SetParams();      

        //ILayout room = new IndoorBoxShape(3, 12, 3, 12).Attributes(new Vector3(-25, 0, 25)).SetParams();
        center.SetLayout(height, mapSetter);
        //room.SetLayout(height, mapSetter);

        PassageSetter(center,height, mapSetter);

        Door newDoor = MonoBehaviour.Instantiate(mapSetter.door);
        newDoor.transform.position = new Vector3(0, height, (center.ReturnBreadth() / 2) - newDoor.transform.localScale.z * 4);     
    }

    void PassageSetter(ILayout center, int height, MapSetter mapSetter)
    {
        IndoorBoxShape passage;

        int dominantAxis = Random.Range(0, 2);
        int sideDecider = Random.Range(0, 2);

        int xRange;
        int zRange;

        if (dominantAxis == 0)
        {
            passage = new IndoorBoxShape(6, 10, 2, 4);
            passage.SetParams();
            if (sideDecider == 0)
            {
                xRange = -center.ReturnWidth() / 2 - passage.width / 2;
            }
            else xRange = center.ReturnWidth() / 2 + passage.width / 2;

            zRange = Random.Range(-center.ReturnBreadth() / 2 + passage.breadth / 2, center.ReturnBreadth() / 2 - passage.breadth / 2);
        }
        else
        {
            passage = new IndoorBoxShape(2, 4, 6, 10);
            passage.SetParams();
            if (sideDecider == 0)
            {
                zRange = -center.ReturnBreadth() / 2 - passage.breadth / 2;
            }
            else zRange = center.ReturnBreadth() / 2 + passage.breadth / 2;
            xRange = Random.Range(-center.ReturnWidth() / 2 + passage.width / 2, center.ReturnWidth() / 2 - passage.width / 2);
        }

        Vector3 relocator = new Vector3(xRange, 0, zRange);
        passage.Attributes(relocator);
        passage.SetLayout(height, mapSetter);
    }


    public ILayout SetParams()
    {
        return this;
    }
}
