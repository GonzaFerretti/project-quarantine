using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TentativeMapInfoKeeper : MonoBehaviour
{
    public List<TentativeMapInfo> mapInfoList;
    public List<string> sceneList;
    public Dictionary<string, TentativeMapInfo> sceneMapAssigner;
    public Dictionary<string, ItemWrapper> itemAssigner;
    public ItemWrapper boltcutter;
    public ItemWrapper bottle;
    public ItemWrapper cornucopia;
    public ItemWrapper firecracker;
    public ItemWrapper firstAidKit;
    public Fence fence;

    private void Start()
    {
        DontDestroyOnLoad(this);
        CloneMapInfo();
        //EventManager.SubscribeToEvent("Enter", SpawnItems);
        EventManager.SubscribeToEvent("Loss", LossBehavior);
        if (sceneMapAssigner == null) sceneMapAssigner = new Dictionary<string, TentativeMapInfo>();
        if (itemAssigner == null) itemAssigner = new Dictionary<string, ItemWrapper>();
        itemAssigner.Add("boltCutter", boltcutter);
        itemAssigner.Add("Bottle", bottle);
        itemAssigner.Add("cornucopia", cornucopia);
        itemAssigner.Add("Firecracker", firecracker);
        itemAssigner.Add("firstAidKit", firstAidKit);

        for (int i = 0; i < sceneList.Count; i++)
        {
            if (mapInfoList[i])
                sceneMapAssigner.Add(sceneList[i], mapInfoList[i]);
        }
        SceneManager.activeSceneChanged += SpawnItems;
    }

    void LossBehavior()
    {
        //EventManager.UnsubscribeToEvent("Enter", SpawnItems);
        EventManager.UnsubscribeToEvent("Loss", LossBehavior);
        SceneManager.activeSceneChanged -= SpawnItems;
    }

    void CloneMapInfo()
    {
        for (int i = 0; i < mapInfoList.Count; i++)
        {
            mapInfoList[i] = mapInfoList[i].Clone();
        }
    }

    void SpawnItems(Scene current, Scene next)
    {
        StartCoroutine(SpawnItemCoroutine());
    }

    IEnumerator UpdateUICoroutine(ModelPlayable player)
    {
        while (!FindObjectOfType<UiManager>())
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        ResourceManager.ReloadUI();
        player.inv.UpdateAllUI();
    }

    IEnumerator SpawnItemCoroutine()
    {
        yield return new WaitForFixedUpdate();

        TentativeMapInfo currentScene = sceneMapAssigner[SceneManager.GetActiveScene().name];

        foreach (CameraMovement cam in FindObjectsOfType<CameraMovement>())
        {
            cam.shouldMove = currentScene.shouldCameraMove;
        }
        ModelPlayable player = GameObject.FindObjectOfType<ModelPlayable>();
        /*player.inv.UpdateAllUI();*/
        for (int i = 0; i < currentScene.items.Count; i++)
        {
            if (currentScene.positions[i] != null)
            {
                ItemWrapper newItem = Instantiate(itemAssigner[currentScene.items[i].name]);
                //newItem.item = currentScene.items[i];
                //newItem.GetComponent<MeshFilter>().mesh = currentScene.items[i].mesh;
                newItem.transform.position = currentScene.positions[i];
                newItem.transform.rotation = Quaternion.Euler(currentScene.rotations[i].x, currentScene.rotations[i].y, currentScene.rotations[i].z);
                //newItem.transform.localScale = currentScene.scales[i];
                //newItem.GetComponent<SphereCollider>().radius = currentScene.radiuses[i];
            }
        }

        for (int i = 0; i < currentScene.fencePositions.Count; i++)
        {
            Fence newFence = Instantiate(fence);
            newFence.transform.position = currentScene.fencePositions[i];
            newFence.transform.rotation = Quaternion.Euler(-90, currentScene.fenceRotations[i].y, currentScene.fenceRotations[i].z);
            if (currentScene.fenceWasBroken[i])
            {
                newFence.switchColliders();
                newFence.animator.Play("Broken");
            }
            else
                newFence.canBeCut = true;
        }

        StartCoroutine(UpdateUICoroutine(player));
    }
}