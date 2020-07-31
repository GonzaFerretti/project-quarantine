using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractablePreviewCameraZoom : IActionPreview
{
    float _waitDuration;
    float _transitionDuration;
    float _distance;

    public InteractablePreviewCameraZoom(float waitDuration, float transitionDuration, float distance)
    {
        _waitDuration = waitDuration;
        _transitionDuration = transitionDuration;
        _distance = distance;
    }

    public virtual void Do(Model m, TextMeshProUGUI tb, InteractableObject obj, bool hasRequiredAction)
    {
        if (!obj.hasBeenPreviewed)
        {
            //something happens
            //(m as ModelPlayable).controller = (m as ModelPlayable).lossController;
            GameObject.Find("Main Camera").GetComponent<CameraMovement>().StartFocusOnPoint(new Vector3(m.transform.position.x,(m as ModelPlayable).standingBodyHeight, m.transform.position.z), _waitDuration, _transitionDuration, -m.transform.forward);
            obj.hasBeenPreviewed = true;
        }
    }
}
