using UnityEngine;

public class ActionEnter : IAction
{
    float _rayDistance;

    public ActionEnter(float rayDistance)
    {
        _rayDistance = rayDistance;
    }

    public void Do(Model m)
    {
        //Animaciones
        ModelPlayable mp = m as ModelPlayable;
        RaycastHit hit;
        Physics.Raycast(m.transform.position + ReturnHeight(mp.bodyHeight), m.transform.forward, out hit, _rayDistance);
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