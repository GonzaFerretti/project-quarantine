using UnityEngine;

public class ActionDuck : IAction
{
    Vector3 _center;
    float _height;

    public ActionDuck(float height, Vector3 center)
    {
        _center = center;
        _height = height;
    }

    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        mh.isDucking = true;
        CapsuleCollider c = m.GetComponent<CapsuleCollider>();
        c.height = _height;
        c.center = _center;
    }
}
