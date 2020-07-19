using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractablePreviewTextBase : IActionPreview
{
    public virtual void Do(Model m, TextMeshProUGUI tb, InteractableObject obj, bool hasRequiredAction)
    {
        if (hasRequiredAction)
        {
            string key = (obj.requiredAction.actionKey) ? obj.requiredAction.actionKey.key.ToString() : "Space";
            tb.text = obj.requiredAction.name + " (" + key + ")";
        }
    }
}
