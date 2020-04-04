using UnityEngine.AI;

public class InteractableOpen : IActionInteractableObject
{
    public void Do(InteractableObject obj)
    {
       NavMeshObstacle navCollider = obj.GetComponent<NavMeshObstacle>();
       navCollider.enabled = !navCollider.enabled;
    }
}
