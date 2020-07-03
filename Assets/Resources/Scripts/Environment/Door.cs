using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    public MapSetter mapSetter;
    public string tentativeSceneName;
    public MapAttributes mapAttributes;
    public MapInfoKeeper mapInfoKeeper;
    public Vector3 targetLocation;

    //public MapAttributes GenerateIndoorAttributes()
    //{
    //    MapAttributes newMap = ScriptableObject.CreateInstance("MapAttributes") as MapAttributes;
    //    newMap.storyAmount = Random.Range(1, 3);
    //    newMap.doorAmount = 2;
    //    newMap.height = 2;
    //    newMap.name = "MapNo " + mapInfoKeeper.indoors.Count;
    //    newMap.mapName = "MapNo " + mapInfoKeeper.indoors.Count;
    //    newMap.layout = new IndoorBoxShape(6,6,6,6).SetParams();
    //    mapInfoKeeper.indoors.Add(newMap);
    //    mapInfoKeeper.mapLinker.Add(newMap.mapName, newMap);
    //    mapInfoKeeper.doorLinker.Add(mapInfoKeeper.currentMap + transform.position, newMap.mapName);
    //    return newMap;
    //}

    protected override void Start()
    {
        base.Start();
        requiredAction.name = "Enter";
        EventManager.SubscribeToEvent("Alert", AlertAction);
        EventManager.SubscribeToEvent("AlertStop", AlertStopAction);
        EventManager.SubscribeToEvent("Enter", EnterAction);
    }

    void AlertAction()
    {
        requiredAction.name = "Locked";

    }

    void AlertStopAction()
    {
        requiredAction.name = "Enter";
    }

    void EnterAction()
    {
        EventManager.UnsubscribeToEvent("Alert", AlertAction);
        EventManager.UnsubscribeToEvent("AlertStop", AlertStopAction);
        EventManager.UnsubscribeToEvent("Enter", EnterAction);
    }

    IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForFixedUpdate();
        SceneManager.LoadScene(tentativeSceneName);
        EventManager.TriggerEvent("Enter");
    }

    public void SceneLoad()
    {
        StartCoroutine(DelayedSceneLoad());
    }
}