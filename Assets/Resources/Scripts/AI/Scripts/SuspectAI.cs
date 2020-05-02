using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Controller/AI/SuspectAI")]
public class SuspectAI : ControllerWrapper, IController
{
    public float targetTreshold;

    public float rotationDuration;
    public float rotationMaxDuration;

    public int currentRotations;
    public int rotationMaxAmount;

    public float degrees;

    ModelPatrol _model;
    Vector3 _target;

    public void AssignModel(Model model)
    {
        _model = model as ModelPatrol;
    }

    public override ControllerWrapper Clone()
    {
        ControllerWrapper clone = CreateInstance("SuspectAI") as ControllerWrapper;
        (clone as SuspectAI).targetTreshold = targetTreshold;
        (clone as SuspectAI).rotationDuration = rotationDuration;
        (clone as SuspectAI).rotationMaxDuration = rotationMaxDuration;
        (clone as SuspectAI).currentRotations = currentRotations;
        (clone as SuspectAI).rotationMaxAmount = rotationMaxAmount;
        (clone as SuspectAI).degrees = degrees;
        return clone;
    }

    public void OnUpdate()
    {
        if (Distance())
        {
            _model.GetComponent<NavMeshAgent>().SetDestination(_target);
        }
        else
        {
            SecondaryPhase();
        }
    }

    void SecondaryPhase()
    {
        if (currentRotations < rotationMaxAmount)
            Rotate();
        else _model.controller = _model.standardController;
    }

    void Rotate()
    {
        if (rotationDuration < rotationMaxDuration)
        {
            _model.transform.Rotate(new Vector3(0, degrees * Time.deltaTime, 0));
            rotationDuration += 1 * Time.deltaTime;
        }
        else
        {
            currentRotations++;
            rotationDuration = 0;
            degrees *= -1;
        }
    }

    bool Distance()
    {
        return Vector3.Distance(_model.transform.position, _target) > targetTreshold;
    }


    public SuspectAI SetTarget(Vector3 target)
    {
        _target = target;
        return this;
    }

    public override void SetController()
    {
        myController = this;
    }
}
