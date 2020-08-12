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
    public float patrolHeight;
    public float normalWaitTime;
    bool alert;

    void Start()
    {
        EventManager.SubscribeToEvent("Alert", CallReinforcements);
        EventManager.SubscribeToEvent("AlertStop", CallReinforcements);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);
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

            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit);

            newPatrol.transform.position = hit.point+ new Vector3(0,patrolHeight,0);
            newPatrol.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(StartNavMesh(newPatrol));
            newPatrol.spawner = this;
            newPatrol.standardController = newPatrol.indoorController;
            StartCoroutine(DelaySetTarget(newPatrol));
            reinforcementAmount--;
            StartCoroutine(SpawnReinforcements(normalWaitTime));
        }
    }

    IEnumerator DelaySetTarget(ModelPatrol newPatrol)
    {
        yield return new WaitForEndOfFrame();
        newPatrol.controller = newPatrol.alertController;
        (newPatrol.alertController as ChaseAI).SetTarget(newPatrol.target.transform.position);
    }

    IEnumerator StartNavMesh(ModelPatrol p)
    {
        yield return new WaitForEndOfFrame();
        p.GetComponent<NavMeshAgent>().enabled = true;
    }

    float SecondsToWait()
    {
        if (reinforcementAmount == maxReinforcementAmount)
            //if(alert) return alertwaittime
            return waitTimeforFirstSummon;
        else
            return normalWaitTime;
    }

    void EnterBehavior()
    {
        EventManager.UnsubscribeToEvent("Alert", CallReinforcements);
        EventManager.UnsubscribeToEvent("AlertStop", CallReinforcements);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }

    void EndAlert()
    {
        reinforcementAmount = maxReinforcementAmount;
        alert = false;
    }

}