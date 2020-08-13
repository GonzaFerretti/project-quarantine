using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractablePreviewWrapper : ScriptableObject
{
    public IActionPreview previewAction;
    public abstract void SetAction();
}
