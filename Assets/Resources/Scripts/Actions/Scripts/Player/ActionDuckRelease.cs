using UnityEngine;

public class ActionDuckRelease : IAction
{
    Vector3 _center;
    float _height;

    public ActionDuckRelease(float height, Vector3 center)
    {
        _center = center;
        _height = height;
    }

    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        mh.isDucking = false;
        CapsuleCollider c = m.GetComponent<CapsuleCollider>();
        c.height = _height;
        c.center = _center;
    }
}
