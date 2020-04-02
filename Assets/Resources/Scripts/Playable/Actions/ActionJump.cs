using UnityEngine;

public class ActionJump : IAction
{
    float _jumpForce;
    public ActionJump(float jumpForce)
    {
        _jumpForce = jumpForce;
    }

    public void Do(ModelChar m)
    {
        Rigidbody rg = m.GetComponent<Rigidbody>();
        rg.AddForce(Vector3.up * _jumpForce);
    }
}
