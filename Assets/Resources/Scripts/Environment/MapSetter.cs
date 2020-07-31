using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetter : MonoBehaviour
{
    public MapAttributes mapAttributes;
    public FloorTiles floorTiles;
    public MapInfoKeeper mapInfoKeeper;
    public Door door;
    GameObject _tileContainer;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreateFloorWrapper()
    {
        StartCoroutine(CreateFloor());
    }

    public IEnumerator CreateFloor()
    {
        yield return new WaitForFixedUpdate();

        mapAttributes.layout.SetLayout(0, this);

        if (mapInfoKeeper.indoors == null) mapInfoKeeper.indoors = new List<MapAttributes>();

        if(!mapInfoKeeper.indoors.Contains(mapAttributes))
        mapInfoKeeper.indoors.Add(mapAttributes);

        mapInfoKeeper.currentMap = mapAttributes.mapName;

        Destroy(gameObject);
    }

    public void CreateTiles(Vector3 position)
    {
        FloorTiles newTile = Instantiate(floorTiles);
        if (_tileContainer == null) _tileContainer = new GameObject("TileContainer");
        newTile.transform.parent = _tileContainer.transform;
        newTile.transform.position = new Vector3(position.x, position.y, position.z);
    }

    public void CreateDoor(Vector3 position, Quaternion rotation, int number)
    {
        Door newDoor = Instantiate(door);
        //newDoor.mapInfoKeeper = mapInfoKeeper;

        //If this door has a link to a map attribute then it assigns that attribute
        if (mapInfoKeeper.doorLinker.ContainsKey(mapInfoKeeper.currentMap + newDoor.transform.position))
         //-   newDoor.mapAttributes = mapInfoKeeper.mapLinker[mapInfoKeeper.doorLinker[mapInfoKeeper.currentMap + newDoor.transform.position]];

        newDoor.transform.position = position;
    }

}
