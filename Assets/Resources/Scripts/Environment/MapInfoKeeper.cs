using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoKeeper : MonoBehaviour
{
    public MapAttributes outdoors;
    public List<MapAttributes> indoors;
    public string currentMap;
    public string previousMap;
    public MapSetter setter;
    public MapSetter newSetter;
    public Dictionary<string, MapAttributes> mapLinker;
    public Dictionary<string, string> doorLinker;

    void Start()
    {
        DontDestroyOnLoad(this);
        mapLinker = new Dictionary<string, MapAttributes>();
        doorLinker = new Dictionary<string, string>();
        if (!newSetter) CreateFirstMap();
    }

    void CreateFirstMap()
    {
        newSetter = Instantiate(setter);
        MapAttributes newMap = ScriptableObject.CreateInstance("MapAttributes") as MapAttributes;
        newMap.doorAmount = 1;
        newMap.layout = new IndoorBoxShape(6,6,6,6).SetParams();
        newMap.name = "MapNo " + indoors.Count;
        newMap.mapName = "MapNo " + indoors.Count;
        newSetter.mapInfoKeeper = this;
        newSetter.mapAttributes = newMap;
        indoors.Add(newMap);
        mapLinker.Add(newMap.mapName, newMap);
        newSetter.CreateFloorWrapper();
    }

}
