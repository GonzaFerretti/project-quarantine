using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotation : IAction
{
    float _speed;
    float _duration;
    float _maxDuration;

    PatrolNode _node;

    public ActionRotation(float speed, float duration)
    {
        _speed = speed;
        _maxDuration = duration;
    }

    public void Do(ModelChar m)
    {
        if (_duration < _maxDuration)
        {
            m.transform.Rotate(0, _speed * _duration, 0);
            _duration += Time.deltaTime;
        }
        else
        {
            _node.currentAction++;
            _duration = 0;
        }        
    }

    public ActionRotation SetNode(PatrolNode p)
    {
        _node = p;
        return this;
    }
}
