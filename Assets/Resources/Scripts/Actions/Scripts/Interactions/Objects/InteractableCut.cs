using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableCut : IActionInteractableObject
{
    public void Do(InteractableObject obj)
    {
        obj.animator.SetBool("ShouldOpen", true);
        obj.GetComponent<Collider>().enabled = false;

        (obj as Fence).switchColliders();

        TentativeMapInfoKeeper infoKeeper = MonoBehaviour.FindObjectOfType<TentativeMapInfoKeeper>();
        TentativeMapInfo currentMapInfo = infoKeeper.sceneMapAssigner[SceneManager.GetActiveScene().name];

        for (int i = 0; i < currentMapInfo.fencePositions.Count; i++)
        {
            if (obj.transform.position == currentMapInfo.fencePositions[i])
                currentMapInfo.fenceWasBroken[i] = true;
        }
    }
}
