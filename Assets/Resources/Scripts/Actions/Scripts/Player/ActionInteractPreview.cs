using UnityEngine;
using TMPro;

public class ActionInteractPreview : IAction
{
    TextMeshProUGUI _textBox;
    float _rayDistance;

    public ActionInteractPreview(float rayDistance, TextMeshProUGUI text)
    {
        _rayDistance = rayDistance;
        _textBox = text;
    }

    public void Do(Model m)
    {
        GameObject go = null;
        float distanceToObject = 0;
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
                distanceToObject = hit.distance;
            }
        }

        ModelChar mc = m as ModelChar;
        if (go && mc.controller != (m as ModelPlayable).redirectController)
        {
            InteractableObject interactable = go.GetComponent<InteractableObject>();
            if (interactable)
            {
                for (int i = 0; i < mc.gainedActions.Count; i++)
                {
                    if (!(mc.gainedActions[i].action is ActionBaseInteract) || (mc.gainedActions[i].action as ActionBaseInteract).interactionDistance > distanceToObject)
                    {
                        interactable.preview.previewAction.Do(m, _textBox, interactable, interactable.requiredAction == mc.gainedActions[i]);
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