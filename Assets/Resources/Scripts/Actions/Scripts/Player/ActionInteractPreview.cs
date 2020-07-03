using UnityEngine;
using TMPro;

public class ActionInteractPreview : IAction
{
    TextMeshProUGUI _textBox;
    float _rayDistance;
    float _angleArc;
    float _arcDensity;

    public ActionInteractPreview(float rayDistance, TextMeshProUGUI text, float arc, float density)
    {
        _rayDistance = rayDistance;
        _textBox = text;
        _angleArc = arc;
        _arcDensity = density;
    }

    public void Do(Model m)
    {
        GameObject go = null;
        CapsuleCollider collider = m.GetComponent<CapsuleCollider>();
        Vector3 startPoint = new Vector3(m.transform.position.x, m.transform.position.y + collider.height * m.transform.localScale.x / 2, m.transform.position.z);
        if ((m is ModelHumanoid) && (m as ModelHumanoid).nearbyObject)
        {
            go = (m as ModelHumanoid).nearbyObject.gameObject;
        }
        else
        {
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(startPoint, m.transform.forward, out hit, _rayDistance);
            /*int i = 0;
            /*string DebugText = "";
            foreach (RaycastHit hitTest in Physics.RaycastAll(startPoint, m.transform.forward, _rayDistance))
            {
                i++;
                DebugText += i + " " + hitTest.collider.gameObject.name + " ";
            }
            Debug.Log(DebugText);*/
            if (hit.collider)
            {
                go = hit.collider.gameObject;
            }
        }
        
        ModelChar mc = m as ModelChar;
        Debug.DrawLine(startPoint, startPoint + m.transform.forward, Color.blue, 2);
        if (go && mc.controller != (m as ModelPlayable).redirectController)
        {
            InteractableObject interactable = go.GetComponent<InteractableObject>();
            if (interactable)
            {
                for (int i = 0; i < mc.gainedActions.Count; i++)
                {
                    if (interactable.requiredAction == mc.gainedActions[i])
                    {
                        _textBox.text = interactable.requiredAction.name + " (F)";
                    }
                    
                    {
                        if (interactable is Fence &&!mc.gainedActions.Contains(interactable.requiredAction))
                            _textBox.text = "I might need to find some Wirecutters to open this...";
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