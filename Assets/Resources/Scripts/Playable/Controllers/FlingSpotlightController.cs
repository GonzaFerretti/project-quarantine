using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Controller/Player/SpotlightController")]
public class FlingSpotlightController : ControllerWrapper, IController
{
    Model _model;
    public void AssignModel(Model model)
    {
        _model = model;
    }

    public override ControllerWrapper Clone()
    {
        return CreateInstance("FlingSpotlightController") as FlingSpotlightController;
    }

    public void OnUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            _model.transform.forward = (hit.point - _model.transform.position).normalized;

        if (_model is FlingSpotLight) (_model as FlingSpotLight).point = hit.point;
    }

    public override void SetController()
    {
        myController = this;
    }
}
