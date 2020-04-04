using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Controller/Action/Vault")]
public class ActionVaultWrapper : ActionWrapper
{

    public override void SetAction()
    {
        action = new ActionVault();
    }
}
