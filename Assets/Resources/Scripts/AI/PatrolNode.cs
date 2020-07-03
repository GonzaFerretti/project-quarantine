using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : MonoBehaviour
{
    public PatrolNode nextNode;
    public NodeAction[] queuedAction;
    public int currentAction;
    public ModelPatrol patrol;
    Dictionary<AIEnum, ControllerWrapper> _controllerDictionary;

    void Start()
    {
        _controllerDictionary = new Dictionary<AIEnum, ControllerWrapper>();
        StartCoroutine(WaitToChangeParams());
    }

    IEnumerator WaitToChangeParams()
    {
        yield return new WaitForEndOfFrame();
        AddDictionaryValues();
        ChangeParams();
    }

    public void ChangeParams()
    {
        if (queuedAction[currentAction].myNextAction == AIEnum.Relocate)
        {
            (patrol.standardController as PatrolAI).SetTarget();
        }
        else if (queuedAction[currentAction].myNextAction == AIEnum.Rotate)
        {
            (patrol.scoutController as ScoutAI).SetParams(queuedAction[currentAction]);
            StartCoroutine((patrol.scoutController as ScoutAI).RotationEnd());
            patrol.animator.SetBool("running", false);
        }else if(queuedAction[currentAction].myNextAction == AIEnum.Guard)
        {
            (patrol.guardController as GuardAI).SetParams(queuedAction[currentAction]);
            patrol.animator.SetBool("running", false);
            patrol.animator.SetBool("isIdle", true);
        }
    }

    void AddDictionaryValues()
    {
        if (!_controllerDictionary.ContainsKey(AIEnum.Relocate))
            _controllerDictionary.Add(AIEnum.Relocate, patrol.standardController);
        if (!_controllerDictionary.ContainsKey(AIEnum.Rotate))
            _controllerDictionary.Add(AIEnum.Rotate, patrol.scoutController);
        if (!_controllerDictionary.ContainsKey(AIEnum.Guard))
            _controllerDictionary.Add(AIEnum.Guard, patrol.guardController);
    }

    public void ChangeBehavior()
    {
        AddDictionaryValues();
        patrol.controller = _controllerDictionary[queuedAction[currentAction].myNextAction];
    }

    private void OnDrawGizmos()
    {
        if (nextNode != null)
            Gizmos.DrawLine(transform.position, nextNode.transform.position);
    }
}