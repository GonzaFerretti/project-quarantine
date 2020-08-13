using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHide : ActionBaseInteract
{
    int _goalLocation;
    int _facingRedirecion;
    float _interactionDistance;
    Item _dragUI;

    public ActionHide(int goalLocation, int facingRedirection, float _interactionDistance, Item dragUI)
    {
        interactionDistance = _interactionDistance;
        _goalLocation = goalLocation;
        _facingRedirecion = facingRedirection;
        _dragUI = dragUI;
    }

    public override void Do(Model m)
    {
        RaycastHit hit;
        Physics.Raycast((m as ModelChar).GetRayCastOrigin(), m.transform.forward, out hit, interactionDistance);
        if (hit.collider)
        {
            if (!((m as ModelPlayable).controller is DragBodyController))
            {
                HidingPlace hidingPlace = hit.collider.gameObject.GetComponent<HidingPlace>();
                if (hidingPlace)
                {
                    ModelPlayable mp = (m as ModelPlayable);
                    if (mp.redirectController.myController == null) mp.redirectController.SetController();
                    RedirectController rc = (mp.redirectController as RedirectController);

                    rc.AssignModel(mp);

                    rc.SetFrontGoal(hidingPlace.transform.position + hidingPlace.transform.forward * _goalLocation);
                    rc.SetInsideGoal(hidingPlace.transform.position + hidingPlace.transform.forward / 4 * _facingRedirecion);
                    rc.SetForward(hidingPlace.transform.forward);
                    rc.SetHidingPlace(hidingPlace);

                    if (hidingPlace.unhideAction.action == null) hidingPlace.unhideAction.SetAction();
                    mp.controller = mp.redirectController;
                    mp.isHidden = (_goalLocation == 1);
                    mp.sm.Play(clip);
                    mp.animator.SetBool("isCrawling", _goalLocation == 1);
                }
            }
            else if (hit.collider.gameObject.GetComponent<HidingPlace>())
            {
                GameObject.Destroy(((m as ModelPlayable).draggingEnemy.gameObject));
                (m as ModelPlayable).controller = (m as ModelPlayable).usualController;
                ModelHumanoid mh = m as ModelHumanoid;
                (m as ModelPlayable).inv.RemoveItem(_dragUI);
                mh.animator.SetBool("isCrawling", false);
                mh.animator.SetBool("isRunning", false);
                (m as ModelPlayable).currentSpeed = (m as ModelPlayable).standardSpeed;
                GameObject.FindObjectOfType<UiManager>().RemoveFromInterface(_dragUI);
                mh.sm.Play(clip);
                mh.animator.SetBool("isLifting", false);
            }
        }
    }
}
