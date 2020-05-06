using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCreator : MonoBehaviour
{
    public TentativeMapInfoKeeper mapInfoKeeper;
    //Tentative
    void Start()
    {
        if (!FindObjectOfType<TentativeMapInfoKeeper>()) Instantiate(mapInfoKeeper);
        Destroy(gameObject);
    }
}
