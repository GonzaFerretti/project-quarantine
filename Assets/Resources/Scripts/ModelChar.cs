using UnityEngine;

public class ModelChar : MonoBehaviour
{
    public ControllerWrapper controller;
    public float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public bool isDucking;

    protected virtual void Start()
    {
        currentSpeed = walkSpeed;
        controller.SetController();
        controller.myController.AssignModel(this);
    }

    protected virtual void Update()
    {
        isDucking = false;
        controller.myController.OnUpdate();
    }
}
