using System.Collections;
using System.Collections.Generic;
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

    public void Do(ModelChar m)
    {
        RaycastHit hit;
        Physics.Raycast(m.transform.position, m.transform.forward, out hit, _rayDistance);

        if (hit.collider)
        {
            InteractableObject interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactable)
            {
                for (int i = 0; i < m.availableActions.Count; i++)
                {
                    if (interactable.requiredAction == m.availableActions[i])
                    {
                        _textBox.text = interactable.requiredAction.ToString();
                    }
                }
            }
            else _textBox.text = "";
        }
        else _textBox.text = "";
    }
}
