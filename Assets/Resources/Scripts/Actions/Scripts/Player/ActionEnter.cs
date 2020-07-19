using UnityEngine;

public class ActionEnter : ActionBaseInteract
{
    public ActionEnter(float _interactionDistance)
    {
        interactionDistance = _interactionDistance;
    }

    public override void Do(Model m)
    {
        //Animaciones
        ModelPlayable mp = m as ModelPlayable;
        RaycastHit hit;
        Physics.Raycast(m.transform.position + ReturnHeight(mp.bodyHeight), m.transform.forward, out hit, interactionDistance);
        ModelChar mc = m as ModelChar;
        if (hit.collider && hit.collider.GetComponent<Door>() && MonoBehaviour.FindObjectOfType<AlertPhaseTimer>() && MonoBehaviour.FindObjectOfType<AlertPhaseTimer>().timer == 0)
        {
            m.transform.position = hit.collider.GetComponent<Door>().targetLocation;
        }
    }

    Vector3 ReturnHeight(float f)
    {
        Vector3 returnHeight = new Vector3(0, f, 0);
        return returnHeight;
    }
}