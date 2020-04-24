using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHide : IAction
{
    float _interactionDistance;

    public ActionHide(float interactionDistance)
    {
        _interactionDistance = interactionDistance;
    }

    public void Do(ModelChar m)
    {
        RaycastHit hit;
        Physics.Raycast(m.transform.position, m.transform.forward, out hit, _interactionDistance);
        if (hit.collider)
        {
            InteractableObject interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactable && interactable.requiredAction is ActionHideWrapper)
            {
                Rigidbody rb = m.GetComponent<Rigidbody>();

                if (rb.constraints == RigidbodyConstraints.FreezeAll)
                {
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    m.transform.position = hit.collider.transform.position + hit.collider.transform.forward;
                    m.currentSpeed = m.walkSpeed;
                    interactable.requiredAction.name = "Hide";
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    m.transform.position = hit.collider.transform.position - hit.collider.transform.forward / 2;
                    m.transform.forward = hit.collider.transform.forward;
                    m.currentSpeed = 0;
                    interactable.requiredAction.name = "Unhide";
                }
            }
        }
    }
}
