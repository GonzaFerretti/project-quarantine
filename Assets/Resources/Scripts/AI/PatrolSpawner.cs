using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpawner : MonoBehaviour
{
    public ModelPatrol modelPatrol;
    public int reinforcementAmount;
    public int maxReinforcementAmount;
    public float waitTimeforFirstSummon;
    //public float alertWaitTime;
    public float normalWaitTime;
    bool alert;
    public ControllerWrapper indoorController;

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
            newPatrol.spawner = this;
            newPatrol.controller = newPatrol.alertController;
            //newPatrol.standardController = indoorController.Clone();
            //newPatrol.standardController.SetController();
            //newPatrol.standardController.myController.AssignModel(newPatrol);
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
