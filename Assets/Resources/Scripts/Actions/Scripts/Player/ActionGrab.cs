using UnityEngine;

public class ActionGrab : IAction
{
    public void Do(Model m)
    {
        ModelHumanoid mh = m as ModelHumanoid;
        if (mh.nearbyObject != null && m is ModelPlayable)
        {
            ModelPlayable model = m as ModelPlayable;
            Vector3 baseDirection = (model.nearbyObject.transform.position - model.transform.position).normalized;
            Vector3 finalDirection = new Vector3(baseDirection.x, 0, baseDirection.z);
            ItemWrapper item = model.nearbyObject.GetComponent<ItemWrapper>();
            item.Interact(model);
            model.transform.forward = finalDirection;
            model.inv.AddItem(item.item);
            model.nearbyObject = null;
        }
    }
}
