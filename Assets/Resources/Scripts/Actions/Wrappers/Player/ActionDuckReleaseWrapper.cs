using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Duck Release")]
public class ActionDuckReleaseWrapper : ActionWrapper
{
    public float height;
    public float radius;
    public Vector3 center;

    public override void SetAction()
    {
        action = new ActionDuckRelease(height, radius, center);
    }
}