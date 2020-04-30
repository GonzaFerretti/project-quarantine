using UnityEngine;
using UnityEngine.UI;

public class ActionInteractPreview : IAction
{
    Text _textBox;
    float _rayDistance;

    public ActionInteractPreview(float rayDistance, Text text)
    {
        _rayDistance = rayDistance;
        _textBox = text;
    }

    public void Do(Model m)
    {
        RaycastHit hit;
        Physics.Raycast(m.transform.position, m.transform.forward, out hit, _rayDistance);
        ModelChar mc = m as ModelChar;
        if (hit.collider && mc.controller != (m as ModelPlayable).redirectController)
        {
            InteractableObject interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactable)
            {
                for (int i = 0; i < mc.availableActions.Count; i++)
                {
                    if (interactable.requiredAction == mc.availableActions[i])
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
