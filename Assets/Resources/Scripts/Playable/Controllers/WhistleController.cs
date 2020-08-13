using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Controller/Player/Whistle Controller")]
public class WhistleController : PlayerController
{
    public float timeMax;
    public float minSize;

    float maxSize = 0;
    float timeStart = 0;
    bool direction = true;
    ModelPlayable mp;
    public override void OnUpdate()
    {
        base.OnUpdate();
        // here we could vary the whistle range. 
        calculateSize();
    }

    public void InitWhistle(float _maxSize)
    {
        maxSize = _maxSize;
        timeStart = Time.time;
        direction = true;
        mp = _model as ModelPlayable;
    }

    public void calculateSize()
    {
        float currentTime = Time.time - timeStart;
        float currentTimeIndex = (direction) ? Mathf.InverseLerp(0, timeMax, currentTime) : 1 - Mathf.InverseLerp(0, timeMax, currentTime);
        float finalSize = Mathf.Lerp(minSize,maxSize,currentTimeIndex);
        mp.rangeIndicator.UpdateSize(finalSize * 2 / mp.transform.localScale.x);
        mp.whistleStrength = finalSize;
        if (currentTimeIndex >= 1 || currentTimeIndex <= 0)
        {
            direction = !direction;
            timeStart = Time.time;
        }
    }
}
