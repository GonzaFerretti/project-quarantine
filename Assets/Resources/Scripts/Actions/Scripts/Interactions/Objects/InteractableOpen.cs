using UnityEngine.AI;

public class InteractableOpen : BaseActionInteractableObject
{
    public override void Do(InteractableObject obj)
    {
       NavMeshObstacle navCollider = obj.GetComponent<NavMeshObstacle>();
       navCollider.enabled = !navCollider.enabled;
    }
}