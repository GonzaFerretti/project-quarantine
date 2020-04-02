﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAI : ControllerWrapper, IController
{
    ModelPatrol _model;
    public void AssignModel(ModelChar model)
    {
        _model = model as ModelPatrol;
    }

    public void OnUpdate()
    {
        _model.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(_model.target.transform.position);
    }

    public override void SetController()
    {
        myController = this;
    }
}