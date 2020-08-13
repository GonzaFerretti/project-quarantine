using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/SniffingAI")]
public class SniffingAI : ControllerWrapper, IController, INeedTargetLocation
{
    ModelDog _model;
    NavMeshAgent _agent;
    Vector3 _target;
    public float sniffWaitTime;
    public float maxDistance;
    public float distanceThreshold;

    public void AssignModel(Model model)
    {
        _model = model as ModelDog;
        _agent = model.GetComponent<NavMeshAgent>();
    }

    public override ControllerWrapper Clone()
    {
        SniffingAI clone = CreateInstance("SniffingAI") as SniffingAI;
        clone.sniffWaitTime = sniffWaitTime;
        clone.maxDistance = maxDistance;
        clone.distanceThreshold = distanceThreshold;
        return clone;
    }

    public void OnUpdate()
    {
        float dis = Vector3.Distance(_model.transform.position, _target);
        if (dis < distanceThreshold)
        {
            _model.StartCoroutine(Sniff());
        }

        if (dis > maxDistance)
        {
            _model.controller = _model.standardController;
        }
    }

    public void Move()
    {
        _model.animator.SetBool("running", true);
        _agent.SetDestination(_target);
    }

    IEnumerator Sniff()
    {
        yield return new WaitForSeconds(sniffWaitTime);
        _model.animator.SetBool("running", false);
        if (_model.currentScentTrail == 0)
            _model.currentScentTrail = _model.scentCreator.scentObjects.Count - 1;
        else
            _model.currentScentTrail -= 1;

        _target = _model.scentCreator.scentObjects[_model.currentScentTrail].transform.position;
        Move();
    }

    public override void SetController()
    {
        myController = this;
    }

    public INeedTargetLocation SetTarget(Vector3 target)
    {
        _target = target;
        return this;
    }
}