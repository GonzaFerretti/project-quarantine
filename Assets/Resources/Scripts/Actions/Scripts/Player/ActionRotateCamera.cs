using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotateCamera : BaseAction
{
    float _directionMultiplier;
    float _step;

    public ActionRotateCamera(float directionMultiplier, float step)
    {
        _directionMultiplier = directionMultiplier;
        _step = step;
    }

    public override void Do(Model m)
    {
        ModelPlayable mp = (m as ModelPlayable);
        if (!mp.mainCam || !mp.secondaryCamera)
        {
            mp.mainCam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
            mp.secondaryCamera = GameObject.Find("minimapCamera").GetComponent<CameraMovement>();
        }
        mp.mainCam.Rotate(_directionMultiplier, _step);
        mp.secondaryCamera.Rotate(_directionMultiplier, _step);
    }
}
