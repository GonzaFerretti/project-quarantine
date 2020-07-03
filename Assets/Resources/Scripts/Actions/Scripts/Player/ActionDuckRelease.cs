using UnityEngine;

public class ActionDuckRelease : IAction
{
    Vector3 _center;
    float _height;
    float _radius;

    public ActionDuckRelease(float height, float radius, Vector3 center)
    {
        _center = center;
        _height = height;
        _radius = radius;
    }
    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        mh.isDucking = false;

        mh.animator.SetBool("isCrawling", false);
        CapsuleCollider c = m.GetComponent<CapsuleCollider>();
        c.height = _height;
        c.center = _center;
        c.radius = _radius;
        if (m is ModelPlayable)
        {
            mh.currentSpeed = mh.standardSpeed;
            (m as ModelPlayable).bodyHeight = (m as ModelPlayable).standingBodyHeight;
        }
    }
}