using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableGrab : IActionInteractableObject
{
    public void Do(InteractableObject obj)
    {
        //tentative
        TentativeMapInfoKeeper infoKeeper = MonoBehaviour.FindObjectOfType<TentativeMapInfoKeeper>();
        int positionInList = 0;
        TentativeMapInfo currentMapInfo = infoKeeper.sceneMapAssigner[SceneManager.GetActiveScene().name];

        for (int i = 0; i < currentMapInfo.items.Count; i++)
        {
            if (currentMapInfo.items[i] == (obj as ItemWrapper))
                positionInList = i;
            break;
        }

        if (currentMapInfo.items.Contains((obj as ItemWrapper).item))
        {
            currentMapInfo.items.Remove((obj as ItemWrapper).item);
            currentMapInfo.positions.Remove(currentMapInfo.positions[positionInList]);
            currentMapInfo.rotations.Remove(currentMapInfo.rotations[positionInList]);
            //currentMapInfo.scales.Remove(currentMapInfo.scales[positionInList]);
        }
        Object.Destroy(obj.gameObject);
    }
}
