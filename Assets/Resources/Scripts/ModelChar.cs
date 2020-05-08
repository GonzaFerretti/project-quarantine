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
    public Animator animator;

    protected virtual void Start()
    {
        currentSpeed = standardSpeed;
        controller.SetController();
        controller.myController.AssignModel(this);
        // https://answers.unity.com/questions/1380820/addcomponent-is-obsolet-what-should-i-do.html
        animator = gameObject.AddComponent<Animator>() as Animator;
    }

    protected virtual void Update()
    {
        controller.myController.OnUpdate();
    }  
}