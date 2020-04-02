using UnityEngine;

public class ModelChar : MonoBehaviour
{
    public ControllerWrapper controller;
    public float currentSpeed;
    public float walkSpeed;
    public float runSpeed;

    protected virtual void Start()
    {
        currentSpeed = walkSpeed;
        controller.SetController();
        controller.myController.AssignModel(this);
    }

    protected virtual void Update()
    {
        controller.myController.OnUpdate();
    }
}
