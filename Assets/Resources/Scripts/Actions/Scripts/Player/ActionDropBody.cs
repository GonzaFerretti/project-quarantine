using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDropBody : BaseAction
{
    Item _dragUI;

    public ActionDropBody(Item dragUI)
    {
        _dragUI = dragUI;
    }

    public override void Do(Model m)
    {
        ModelPlayable mp = (m as ModelPlayable);
        mp.controller = (m as ModelPlayable).usualController;
        mp.draggingEnemy.ragdoll.KnockOut(Vector3.zero);
        mp.inv.RemoveItem(_dragUI);
        mp.animator.SetBool("isCrawling", false);
        mp.animator.SetBool("isRunning", false);
        mp.currentSpeed = mp.standardSpeed;
        GameObject.FindObjectOfType<UiManager>().RemoveFromInterface(_dragUI);
        mp.animator.SetBool("isLifting", false);
    }
}
