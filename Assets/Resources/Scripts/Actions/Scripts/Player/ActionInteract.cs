using UnityEngine;

public class ActionInteract : IAction
{
    float _rayDistance;
    float _angleArc;
    float _arcDensity;

    public ActionInteract(float rayDistance, float arc, float density)
    {
        _rayDistance = rayDistance;
        _angleArc = arc;
        _arcDensity = density;
    }

    public void Do(Model m)
    {
        RaycastHit hit = new RaycastHit();
        GameObject hitObject = null;
        CapsuleCollider collider = m.GetComponent<CapsuleCollider>();
        Vector3 startPoint = new Vector3(m.transform.position.x, m.transform.position.y + collider.height * m.transform.localScale.x / 2, m.transform.position.z);
        if ((m is ModelHumanoid) && (m as ModelHumanoid).nearbyObject)
        {
            hitObject = (m as ModelHumanoid).nearbyObject.gameObject;
        }
        else
        {
            Physics.Raycast(startPoint, m.transform.forward, out hit, _rayDistance);
            if (hit.collider)
            { 
                hitObject = hit.transform.gameObject;
            }
        }
        ModelChar mc = m as ModelChar;
        //Debug.DrawLine(startPoint, startPoint + m.transform.forward, Color.red, 2);
        if (hit.collider || hitObject)
        {
            InteractableObject interactable = hitObject.GetComponent<InteractableObject>();
            if (interactable)
            {
                float distanceToObject = hit.distance;
                for (int i = 0; i < mc.gainedActions.Count; i++)
                {
                    if (interactable.requiredAction == mc.gainedActions[i])
                    {
                        if (!(mc.gainedActions[i].action is ActionBaseInteract) || (mc.gainedActions[i].action as ActionBaseInteract).interactionDistance > distanceToObject)
                        { 
                            mc.gainedActions[i].action.Do(m);
                            interactable.Interact(m as ModelPlayable);
                        }
                    }
                }
            }
        }
    }

    private float LongestPossibleRoute(Collider objectCol)
    {
        //Calculates the longest possible distance to traverse an object, its collider's bounding box hypotenuse.
        float sizeX = objectCol.bounds.size.x;
        float sizeZ = objectCol.bounds.size.z;
        float result = Mathf.Sqrt(Mathf.Pow(sizeX, 2) + Mathf.Pow(sizeZ, 2));
        return result;
    }
}
