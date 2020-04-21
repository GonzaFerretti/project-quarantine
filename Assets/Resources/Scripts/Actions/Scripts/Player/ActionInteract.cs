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
            if (interactable)
            {
                for (int i = 0; i < m.availableActions.Count; i++)
                {
                    if(interactable.requiredAction == m.availableActions[i])
                    interactable.Interact(m as ModelPlayable);
                }
            }
        }
    }
}
