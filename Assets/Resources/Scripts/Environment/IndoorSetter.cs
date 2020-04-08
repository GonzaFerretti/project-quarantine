using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IndoorSetter : MonoBehaviour
{
    public IndoorAttributes indoorAttributes;
    public GameObject floorTiles;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //hacer SOLID
    }

    public void CreateFloorWrapper()
    {
        StartCoroutine(CreateFloor());
    }

    public IEnumerator CreateFloor()
    {
        yield return new WaitForFixedUpdate();
        floorTiles = Resources.Load<GameObject>("GameObjects/PlaceholderTile");

            indoorAttributes.layout.SetLayout(0,this);
    }
}
