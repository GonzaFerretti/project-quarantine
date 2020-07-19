using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Environment/Objects/Preview/Fence")]
public class InteractableFencePreviewWrapper : InteractablePreviewWrapper
{
    public override void SetAction()
    {
        previewAction = new InteractableFencePreview();
    }
}
