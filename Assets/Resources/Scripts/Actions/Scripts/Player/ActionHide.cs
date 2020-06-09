using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHide : IAction
{
    float _interactionDistance;
    int _goalLocation;
    int _facingRedirecion;

    public ActionHide(int goalLocation, int facingRedirection, float interactionDistance)
    {
        _interactionDistance = interactionDistance;
        _goalLocation = goalLocation;
        _facingRedirecion = facingRedirection;
    }

    public void Do(Model m)
    {
        RaycastHit hit;
        Physics.Raycast((m as ModelChar).GetRayCastOrigin(), m.transform.forward, out hit, _interactionDistance);
        if (hit.collider)
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
                mp.animator.SetBool("isCrawling", _goalLocation == 1);
            }
        }
    }
}
