using UnityEngine;
using UnityEngine.UI;

public class ActionInteractPreview : IAction
{
    Text _textBox;
    float _rayDistance;
    float _angleArc;
    float _arcDensity;

    public ActionInteractPreview(float rayDistance, Text text, float arc, float density)
    {
        _rayDistance = rayDistance;
        _textBox = text;
        _angleArc = arc;
        _arcDensity = density;
    }

    public void Do(Model m)
    {
        RaycastHit hit = new RaycastHit();
        CapsuleCollider collider = m.GetComponent<CapsuleCollider>();
        Vector3 startPoint = new Vector3(m.transform.position.x, m.transform.position.y + collider.height * m.transform.localScale.x / 2, m.transform.position.z);
        if ((m is ModelHumanoid) && (m as ModelHumanoid).nearbyObject)
        {
            float forwardAngle = Vector2.SignedAngle(new Vector2(1, 0), new Vector2(m.transform.forward.x, m.transform.forward.z));
            float longestPossibleRay = LongestPossibleRoute((m as ModelPlayable).GetComponent<Collider>());
            for (float i = -_angleArc / 2 + forwardAngle; i <= _angleArc / 2 + forwardAngle; i += _arcDensity)
            { 
                float angle = i * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle);
                float z = Mathf.Sin(angle);
                Vector3 rayDirection = new Vector3(x, 0, z);
                Debug.DrawLine(startPoint, startPoint + rayDirection * longestPossibleRay, Color.red, Time.deltaTime);
                Physics.Raycast(startPoint, rayDirection, out hit, longestPossibleRay);
                if (hit.collider && hit.collider.gameObject.GetComponent<ItemWrapper>())
                {
                break;
                }
            }
        }
        else
        {
            Physics.Raycast(startPoint, m.transform.forward, out hit, _rayDistance);
            hit = (hit.collider && hit.collider.gameObject.GetComponent<ItemWrapper>()) ? new RaycastHit() : hit;
        }
        ModelChar mc = m as ModelChar;
        if (hit.collider && mc.controller != (m as ModelPlayable).redirectController)
        {
            InteractableObject interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactable)
            {
                for (int i = 0; i < mc.gainedActions.Count; i++)
                {
                    if (interactable.requiredAction == mc.gainedActions[i])
                    {
                        _textBox.text = interactable.requiredAction.name;
                    }
                }
            }
            else _textBox.text = "";
        }
        else _textBox.text = "";
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
