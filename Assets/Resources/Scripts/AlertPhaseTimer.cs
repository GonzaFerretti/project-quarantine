
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPhaseTimer : MonoBehaviour
{
    public float maxTimer;
    public float timer;


    private void Start()
    {
        EventManager.SubscribeToEvent("Alert", ActivateAlert);
    }

    private void ActivateAlert()
    {
        timer = maxTimer;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            timer = 0;
            DeactivateAlert();
        }
    }


    private void DeactivateAlert()
    {
        EventManager.TriggerEvent("AlertStop");
    }
}
