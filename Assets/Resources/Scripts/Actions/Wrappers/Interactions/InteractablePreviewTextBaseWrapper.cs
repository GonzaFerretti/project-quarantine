using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Environment/Objects/Preview/TextBase")]
public class InteractablePreviewTextBaseWrapper : InteractablePreviewWrapper
{
    public override void SetAction()
    {
        previewAction = new InteractablePreviewTextBase();
    }
}
