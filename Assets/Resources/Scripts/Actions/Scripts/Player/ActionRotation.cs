using UnityEngine;

public class ActionRotation : IAction
{
    float _speed;
    Vector3 _quaternion;

    public ActionRotation(float speed, Vector3 quaternion)
    {
        _speed = speed;
        _quaternion = quaternion;
    }

    public void Do(Model m)
    {
        m.transform.Rotate(_quaternion * _speed * Time.deltaTime);
    }
}
