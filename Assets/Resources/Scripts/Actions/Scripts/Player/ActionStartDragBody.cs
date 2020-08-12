using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStartDragBody : ActionBaseInteract
{
    Item _dragUI;
    public ActionStartDragBody(float _interactionDistance, Item dragUI)
    {
        interactionDistance = _interactionDistance;
        _dragUI = dragUI;
    }

    public override void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        mh.animator.SetBool("isLifting", true);
        ModelPlayable mp = (m as ModelPlayable);
        mp.inv.AddItem(_dragUI);
        RaycastHit hit = new RaycastHit();
        CapsuleCollider collider = m.GetComponent<CapsuleCollider>();
        Vector3 startPoint = new Vector3(m.transform.position.x, m.transform.position.y + collider.height * m.transform.localScale.x / 2, m.transform.position.z);
        Physics.Raycast(startPoint, m.transform.forward, out hit, interactionDistance);
        mp.draggingEnemy = hit.transform.gameObject.GetComponent<ModelPatrol>();
        mp.controller = mp.dragBodyController;
        mp.animator.SetBool("isCrawling", false);
        mp.draggingEnemy.animator.SetTrigger("carry");
        mp.draggingEnemy.ragdoll.PickUp();
    }
}
