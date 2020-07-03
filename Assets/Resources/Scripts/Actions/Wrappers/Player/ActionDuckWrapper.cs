using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Duck")]
public class ActionDuckWrapper : ActionWrapper
{
    public float height;
    public float radius;
    public Vector3 center;

    public override void SetAction()
    {
        action = new ActionDuck(height, radius, center);
    }
}
