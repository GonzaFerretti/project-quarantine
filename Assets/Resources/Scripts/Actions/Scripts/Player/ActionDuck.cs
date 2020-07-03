using UnityEngine;

public class ActionDuck : IAction
{
    Vector3 _center;
    float _height;
    float _radius;

    public ActionDuck(float height, float radius, Vector3 center)
    {
        _center = center;
        _height = height;
        _radius = radius;
    }

    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        mh.isDucking = true;
        mh.animator.SetBool("isCrawling", true);        

        if (m is ModelPlayable)
        { 
            mh.currentSpeed = (m as ModelPlayable).myAttributes.sneakSpeed;
            (m as ModelPlayable).bodyHeight = (m as ModelPlayable).duckingBodyHeight;
            CapsuleCollider _collider = m.GetComponent<CapsuleCollider>();
            _collider.center = _center;
            _collider.radius = _radius;
            _collider.height = _height;
        }
    }
}