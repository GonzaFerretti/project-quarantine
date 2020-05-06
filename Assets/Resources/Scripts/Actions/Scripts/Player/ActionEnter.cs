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
        RaycastHit hit;
        Physics.Raycast(m.transform.position, m.transform.forward, out hit, _rayDistance);
        ModelChar mc = m as ModelChar;
        if (hit.collider)
        {
            if (hit.collider.GetComponent<Door>())
            {
                 m.transform.position = hit.collider.GetComponent<Door>().targetLocation;
            }
        }

        Debug.Log("Enter");
    }
}
