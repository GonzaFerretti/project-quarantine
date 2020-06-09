using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/SpotlightController")]
public class FlingSpotlightController : ControllerWrapper, IController
{
    FlingSpotLight _model;
    public float height;

    public void AssignModel(Model model)
    {
        _model = model as FlingSpotLight;
    }

    public override ControllerWrapper Clone()
    {
        return CreateInstance("FlingSpotlightController") as FlingSpotlightController;
    }

    public void OnUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 _modelDistVector = new Vector3(_model.transform.position.x, 0, _model.transform.position.z);
        Vector3 _playableDistVector = new Vector3(_model.modelPlayable.transform.position.x, 0, _model.modelPlayable.transform.position.z);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mPos = _model.modelPlayable.transform.position;
            Vector3 direction = new Vector3(hit.point.x - mPos.x,0, hit.point.z - mPos.z);
            Vector3 clampedDirection = Vector3.ClampMagnitude(direction, _model.modelPlayable.strength);
            _model.transform.position = new Vector3(mPos.x + clampedDirection.x, hit.point.y + height, mPos.z + clampedDirection.z);
        }

        if (_model is FlingSpotLight) (_model as FlingSpotLight).point = hit.point;
    }

    public override void SetController()
    {
        myController = this;
    }
}
