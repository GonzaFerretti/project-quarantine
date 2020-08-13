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
    }

    protected virtual void Update()
    {
        controller.myController.OnUpdate();
    } 

    public Vector3 GetRayCastOrigin()
    {
        Vector3 origin = (GetComponent<CapsuleCollider>()) ? new Vector3(transform.position.x, transform.position.y + GetComponent<CapsuleCollider>().height * transform.localScale.x / 2, transform.position.z) : transform.position;
        return origin;
    }
}