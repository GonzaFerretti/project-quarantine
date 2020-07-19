using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Vault")]
public class ActionVaultWrapper : ActionBaseInteractWrapper
{
    public float vaultHeight;
    public float objectiveOffset;
    public float distanceModifierMin;
    public override void SetAction()
    {
        action = new ActionVault(vaultHeight, interactionDistance, objectiveOffset, distanceModifierMin);
    }
}
