using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public TentativeMapInfoKeeper mapInfoKeeper;
    public ActionMapWrapper myAction;

    private void Start()
    {
        if (myAction)
        {
            myAction = myAction.Clone();
            myAction.SetAction();
        }
        StartCoroutine(FindMapInfoKeeper());
    }

    IEnumerator FindMapInfoKeeper()
    {
        yield return new WaitForFixedUpdate();
        if (mapInfoKeeper == null)
            mapInfoKeeper = FindObjectOfType<TentativeMapInfoKeeper>();

        if (!mapInfoKeeper.sceneMapAssigner[SceneManager.GetActiveScene().name].enteredBefore)
        {
            if (myAction)
                myAction.myMapAction.Do();             
            
            mapInfoKeeper.sceneMapAssigner[SceneManager.GetActiveScene().name].enteredBefore = true;
            Destroy(gameObject);
        }
        else StartCoroutine(FindMapInfoKeeper());
    }
}