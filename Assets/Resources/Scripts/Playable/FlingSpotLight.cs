using UnityEngine;

public class FlingSpotLight : Model
{
    public ModelPlayable modelPlayable;
    Light _myLight;
    public Vector3 point;
    public ControllerWrapper controller; 

    private void Awake()
    {
        modelPlayable = GetComponentInParent<ModelPlayable>();
        _myLight = GetComponent<Light>();
        _myLight.range = modelPlayable.myAttributes.strength;
        controller.SetController();
        controller.myController.AssignModel(this);
    }

    private void Update()
    {
        controller.myController.OnUpdate();
    }
}
