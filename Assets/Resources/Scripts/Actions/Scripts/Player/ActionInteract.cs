using UnityEngine;

public class ActionInteract : IAction
{
    float _rayDistance;

    public ActionInteract(float rayDistance)
    {
        _rayDistance = rayDistance;
    }

    public void Do(ModelChar m)
    {
        RaycastHit hit;
        Physics.Raycast(m.transform.position,m.transform.forward, out hit,_rayDistance);
        if (hit.collider)
        {
            InteractableObject interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactable) interactable.Interact(m as ModelPlayable);
        }
    }
}
