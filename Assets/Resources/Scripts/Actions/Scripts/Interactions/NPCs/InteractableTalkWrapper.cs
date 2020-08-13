using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/Objects/Talk")]
public class InteractableTalkWrapper : InteractableFeedbackWrapper
{
    public override void SetAction()
    {
        action = new InteractableTalk();
    }
}
