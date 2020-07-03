using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCon : MonoBehaviour
{
    public List<string> reqKeys;
    public List<string> confirmed;
    public GameObject winWindow;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForFixedUpdate();
        reqKeys = new List<string>(ResourceManager.requiredResources.Keys); 
        
        confirmed = new List<string>();
        for (int i = 0; i < reqKeys.Count; i++)
        {
            if (ResourceManager.currentResources[reqKeys[i]] >= ResourceManager.requiredResources[reqKeys[i]])
            {
                if (!confirmed.Contains(reqKeys[i]))
                    confirmed.Add(reqKeys[i]);
            }
        }
        if (confirmed.Count == reqKeys.Count)
            WinGame();
    }

    void WinGame()
    {
        Time.timeScale = 0;
        winWindow.SetActive(true);
    }
}