using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSpawner : MonoBehaviour
{
    public ModelPatrol modelPatrol;
    public int reinforcementAmount;
    public int maxReinforcementAmount;
    public float waitTimeforFirstSummon;
    //public float alertWaitTime;
    public float normalWaitTime;
    bool alert;

    void Start()
    {
        EventManager.SubscribeToEvent("Alert", CallReinforcements);
        EventManager.SubscribeToEvent("AlertStop", CallReinforcements);
        reinforcementAmount = maxReinforcementAmount;
    }

    void CallReinforcements()
    {
        if(!alert)
        {
            StartCoroutine(SpawnReinforcements(waitTimeforFirstSummon));
            alert = true;
        }
    }

    IEnumerator SpawnReinforcements(float time)
    {
        yield return new WaitForSeconds(SecondsToWait());
        if (reinforcementAmount > 0)
        {
            ModelPatrol newPatrol = Instantiate(modelPatrol);
            newPatrol.transform.position = transform.position;
            newPatrol.controller = newPatrol.alertController;
            newPatrol.spawner = this;
            newPatrol.standardController = newPatrol.indoorController;

            reinforcementAmount--;
            StartCoroutine(SpawnReinforcements(normalWaitTime));
        }
    }

    float SecondsToWait()
    {
        if (reinforcementAmount == maxReinforcementAmount)
            //if(alert) return alertwaittime
            return waitTimeforFirstSummon;
        else
            return normalWaitTime;
    }

    void EndAlert()
    {
        reinforcementAmount = maxReinforcementAmount;
        alert = false;
    }

}