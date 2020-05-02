using UnityEngine;

public class ActionPatrolRotation : IAction
{
    float _speed;
    float _duration;
    float _maxDuration;
    Vector3 _quaternion;

    PatrolNode _node;

    public ActionPatrolRotation(float speed, float duration, Vector3 quaternion)
    {
        _speed = speed;
        _maxDuration = duration;
        _quaternion = quaternion;
    }

    public void Do(Model m)
    {
        if (_duration < _maxDuration)
        {
            m.transform.Rotate(_quaternion * _speed * _duration);
            _duration += Time.deltaTime;
        }
        else
        {
            _node.currentAction++;
            _duration = 0;
        }        
    }

    public ActionPatrolRotation SetNode(PatrolNode p)
    {
        _node = p;
        return this;
    }
}
