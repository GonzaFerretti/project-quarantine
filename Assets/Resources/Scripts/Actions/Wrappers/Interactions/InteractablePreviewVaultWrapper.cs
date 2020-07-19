using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Environment/Objects/Preview/Vault")]
public class InteractablePreviewVaultWrapper : InteractablePreviewWrapper
{
    public float vaultHeight;
    public float vaultCheckDistance;
    public float objectiveOffset;
    public float distanceModifierMin;
    public override void SetAction()
    {
        previewAction = new InteractableVaultPreview(vaultHeight,vaultCheckDistance,objectiveOffset,distanceModifierMin);
    }
}

