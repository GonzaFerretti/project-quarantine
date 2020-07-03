using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossMenuManager : MonoBehaviour
{
    public GameObject lossBox;
    public float lossDelay;

    private void Start()
    {
        EventManager.SubscribeToEvent("Loss",LossBehavior);
        EventManager.SubscribeToEvent("LossCancel", LossCancel);
        EventManager.SubscribeToEvent("Enter", EnterBehavior);
    }

    void LossBehavior()
    {
        if(this!= null)
        {
            StartCoroutine(MyLoss());
        }
    }

    IEnumerator MyLoss()
    {
        yield return new WaitForSeconds(lossDelay);
        Time.timeScale = 0;
        lossBox.SetActive(true);
    }

    void LossCancel()
    {
        StopAllCoroutines();
    }

    void EnterBehavior()
    {
        EventManager.UnsubscribeToEvent("Loss", LossBehavior);
        EventManager.UnsubscribeToEvent("LossCancel", LossCancel);
        EventManager.UnsubscribeToEvent("Enter", EnterBehavior);
    }

    public void LossConfirm()
    {
        Time.timeScale = 1;
        Destroy(FindObjectOfType<ModelPlayable>().gameObject);
        try
        {

            Destroy(FindObjectOfType<RangeIndicator>().gameObject);
        }
        catch { }
        try
        {

            Destroy(FindObjectOfType<RangeIndicator>().gameObject);
        }
        catch { }
        Destroy(FindObjectOfType<TentativeMapInfoKeeper>().gameObject);
        SceneManager.LoadScene("DebugDelDebug");
    }
}
