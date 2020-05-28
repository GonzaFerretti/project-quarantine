using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelAssistant : MonoBehaviour
{
    //Settings
    public float updateRate = 0;
    private float updateRateTime;
    public MonoBehaviour[] sampleClasses;
    public GameObject[][] groups;
    
    private void Update()
    {
        CheckObjects();
    }

    void CheckObjects()
    {
       if (updateRateTime < updateRate)
        {
            updateRateTime += Time.deltaTime;
        }
       else if (updateRate != 0)
        {
            updateRateTime = 0;
            UpdateObjects();
        }
    }

    void UpdateObjects()
    {
        groups = new GameObject[sampleClasses.Length][];
        for(int i = 0; i<sampleClasses.Length;i++)
        {
            MonoBehaviour sample = sampleClasses[i];
            Object[] foundObjects = FindObjectsOfType(sample.GetType());
            if (foundObjects.Length > 0)
            {
                GameObject[] gameObjects = new GameObject[foundObjects.Length];
                Debug.Log(gameObjects.Length);
                for (int f = 0; f < foundObjects.Length; f++)
                {
                    gameObjects[f] = (foundObjects[f] as Component).gameObject;
                }
                groups[i] = gameObjects;
            }
            else
            {
                Debug.LogWarning("Didn't find any GameObjects containing the object " + sample.GetType());
            }
        }
    }
}
