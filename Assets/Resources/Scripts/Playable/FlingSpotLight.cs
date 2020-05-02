using UnityEngine;

public class FlingSpotLight : Model
{
    ModelPlayable _modelPlayable;
    Light _myLight;
    public Vector3 point;
    public ControllerWrapper controller; 

    private void Awake()
    {
        _modelPlayable = GetComponentInParent<ModelPlayable>();
        _myLight = GetComponent<Light>();
        _myLight.range = _modelPlayable.myAttributes.strength;
        controller.SetController();
        controller.myController.AssignModel(this);
    }

    private void Update()
    {
        controller.myController.OnUpdate();
    }
}
