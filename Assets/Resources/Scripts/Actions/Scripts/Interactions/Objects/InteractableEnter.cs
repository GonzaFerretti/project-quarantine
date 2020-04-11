using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableEnter : IActionInteractableObject
{
    InteractableEnterWrapper _wrapper;

    public InteractableEnter(InteractableEnterWrapper wrapper)
    {
        _wrapper = wrapper;
    }

    public void Do(InteractableObject obj)
    {
        Door door = obj as Door;

        if (door.indoorAttributes == null)
        {
            door.indoorAttributes = door.GenerateIndoorAttributes();
        }

        IndoorSetter indoorSetter = MonoBehaviour.Instantiate(door.indoorSetter);
        indoorSetter.indoorAttributes = door.indoorAttributes;

        if(SceneManager.GetActiveScene().name == _wrapper.scenes[0])
        SceneManager.LoadScene(_wrapper.scenes[1]);
        else
        SceneManager.LoadScene(_wrapper.scenes[0]);

        indoorSetter.CreateFloorWrapper();
    }
}
