using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCreator : MonoBehaviour
{
    public MapInfoKeeper mapInfoKeeper;
    //Tentative
    void Start()
    {
        if (!FindObjectOfType<MapInfoKeeper>()) Instantiate(mapInfoKeeper);
        Destroy(gameObject);
    }
}
