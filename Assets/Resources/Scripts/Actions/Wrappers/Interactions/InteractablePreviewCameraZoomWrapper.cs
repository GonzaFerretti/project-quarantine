using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/Objects/Preview/ZoomView")]
public class InteractablePreviewCameraZoomWrapper : InteractablePreviewWrapper
{
    public float waitDuration;
    public float transitionDuration;
    public float distance;

    public override void SetAction()
    {
        previewAction = new InteractablePreviewCameraZoom(waitDuration, transitionDuration, distance);
    }
}
