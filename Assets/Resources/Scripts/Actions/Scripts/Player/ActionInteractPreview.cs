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
        
        float forwardAngle = Vector3.Angle(Vector3.right,m.transform.forward);
        Debug.Log(forwardAngle);
        for (float i =-_angleArc/2 ; i <=_angleArc/ 2; i += _arcDensity)
        {
            float angle = i * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);
            Vector3 rayDirection = new Vector3(x, 0, z);
            Debug.DrawLine(m.transform.position, m.transform.position + rayDirection, Color.red, Time.deltaTime);
        }
        RaycastHit hit;
        Physics.Raycast(m.transform.position, m.transform.forward, out hit, _rayDistance);
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
}
