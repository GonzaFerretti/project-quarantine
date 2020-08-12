using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Capture")]
public class ActionCaptureWrapper : ActionWrapper
{
    public LayerMask layerMask;

    public override void SetAction()
    {
        action = new ActionCapture(layerMask);
    }

    public ActionCaptureWrapper Clone()
    {
        ActionCaptureWrapper clone = CreateInstance("ActionCaptureWrapper") as ActionCaptureWrapper;
        clone.layerMask = layerMask;
        return clone;
    }
}