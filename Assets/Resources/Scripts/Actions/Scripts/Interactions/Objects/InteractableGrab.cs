using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableGrab : IActionInteractableObject
{
    public void Do(InteractableObject obj)
    {
        //tentative
        //TentativeMapInfoKeeper test = MonoBehaviour.FindObjectOfType<TentativeMapInfoKeeper>();
        //if(test.sceneMapAssigner[SceneManager.GetActiveScene().name].items.Contains((obj as ItemWrapper).item))
        //test.sceneMapAssigner[SceneManager.GetActiveScene().name].items.Remove((obj as ItemWrapper).item);
        Object.Destroy(obj.gameObject);
    }
}
