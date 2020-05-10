using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Vault")]
public class ActionVaultWrapper : ActionWrapper
{
    public float vaultDuration;
    public float vaultHeight;
    public float vaultCheckDistance;
    public override void SetAction()
    {
        action = new ActionVault(vaultDuration,vaultHeight, vaultCheckDistance);
    }
}
