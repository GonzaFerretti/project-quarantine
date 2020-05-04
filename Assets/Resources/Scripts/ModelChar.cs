using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelChar : Model
{
    public ControllerWrapper controller;
    public float currentSpeed;
    public float standardSpeed;
    public float strength;
    public List<ActionWrapper> gainedActions;
    public List<ActionKeyLinks> gainedActionKeyLinks; 
    public FlingObject flingObject;

    protected virtual void Start()
    {
        currentSpeed = standardSpeed;
        controller.SetController();
        controller.myController.AssignModel(this);
    }

    protected virtual void Update()
    {
        controller.myController.OnUpdate();
    }  
}