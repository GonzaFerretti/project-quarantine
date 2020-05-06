using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TentativeMapInfoKeeper : MonoBehaviour
{
    public List<TentativeMapInfo> mapInfoList;
    public List<string> sceneList;
    public Dictionary<string, TentativeMapInfo> sceneMapAssigner;
    public ItemWrapper item;

    private void Start()
    {
        DontDestroyOnLoad(this);
        CloneMapInfo();
        EventManager.SubscribeToEvent("Enter", SpawnItems);
        if (sceneMapAssigner == null) sceneMapAssigner = new Dictionary<string, TentativeMapInfo>();

        for (int i = 0; i < sceneList.Count; i++)
        {
            if (mapInfoList[i])
                sceneMapAssigner.Add(sceneList[i], mapInfoList[i]);
        }
    }

    void CloneMapInfo()
    {
        for (int i = 0; i < mapInfoList.Count; i++)
        {
            mapInfoList[i] = mapInfoList[i].Clone();
        }
    }

    void SpawnItems()
    {
        StartCoroutine(SpawnItemCoroutine());
    }

    IEnumerator SpawnItemCoroutine()
    {
        yield return new WaitForFixedUpdate();
        TentativeMapInfo currentScene = sceneMapAssigner[SceneManager.GetActiveScene().name];
        for (int i = 0; i < currentScene.items.Count; i++)
        {
            if (currentScene.positions[i] != null)
            {
                ItemWrapper newItem = Instantiate(item);
                newItem.item = currentScene.items[i];
                newItem.transform.position = currentScene.positions[i];
            }
        }
    }

}
