using UnityEngine;

public class ActionInteract : IAction
{
    float _rayDistance;

    public ActionInteract(float rayDistance)
    {
        _rayDistance = rayDistance;
    }

    public void Do(Model m)
    {
        RaycastHit hit;
        Physics.Raycast(m.transform.position, m.transform.forward, out hit, _rayDistance);
        ModelChar mc = m as ModelChar;
        if (hit.collider)
        {
            InteractableObject interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactable)
            {
                for (int i = 0; i < mc.gainedActions.Count; i++)
                {
                    if (interactable.requiredAction == mc.gainedActions[i])
                    {
                        mc.gainedActions[i].action.Do(m);
                        interactable.Interact(m as ModelPlayable);
                    }
                }
            }
        }
    }
}
