using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Player/Hiding Action Controller")]
public class HidingActionController : ControllerWrapper, IController
{
    ModelPlayable _model;
    Vector3 _goal;
    public float threshold;
    public float speed;
    public float amplitude;
    public float frequency;
    public float origin;
    public float dur;

    public void AssignModel(Model model)
    {
        _model = model as ModelPlayable;
    }

    public override ControllerWrapper Clone()
    {
        HidingActionController clone = CreateInstance("HidingActionController") as HidingActionController;
        clone.threshold = threshold;
        clone.speed = speed;
        clone.amplitude = amplitude;
        clone.origin = origin;
        clone.frequency = frequency;
        return clone;
    }

    public void OnUpdate()
    {
        if (Vector3.Distance(_model.transform.position, _goal) > threshold)
        {
            Vector3 curPos = _model.transform.position;
            dur += Time.deltaTime;
            curPos += new Vector3((_goal - _model.transform.position).normalized.x, Mathf.Sin(dur + origin) * amplitude, (_goal - _model.transform.position).normalized.z) * speed * Time.deltaTime;
            _model.transform.position = curPos;
        }
        else
        {
            _model.controller = _model.hideController;
        }
    }

    public HidingActionController SetGoal(Vector3 goal)
    {
        _goal = goal;
        return this;
    }

    public override void SetController()
    {
        myController = this;
    }
}