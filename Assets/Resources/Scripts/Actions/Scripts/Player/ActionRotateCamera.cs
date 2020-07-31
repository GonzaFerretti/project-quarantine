using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotateCamera : IAction
{
    float _directionMultiplier;
    float _step;

    public ActionRotateCamera(float directionMultiplier, float step)
    {
        _directionMultiplier = directionMultiplier;
        _step = step;
    }

    public void Do(Model m)
    {
        ModelPlayable mp = (m as ModelPlayable);
        mp.mainCam.Rotate(_directionMultiplier, _step);
        mp.secondaryCamera.Rotate(_directionMultiplier, _step);
    }
}
