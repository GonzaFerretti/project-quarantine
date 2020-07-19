using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableFencePreview : InteractablePreviewTextBase
{
    public override void Do(Model m, TextMeshProUGUI tb, InteractableObject obj, bool hasRequiredAction)
    {
        base.Do(m, tb, obj, hasRequiredAction);
        if (!(m as ModelChar).gainedActions.Contains(obj.requiredAction) && !hasRequiredAction)
        { 
            tb.text = "I might need to find some Wirecutters to open this...";
        }
    }
}