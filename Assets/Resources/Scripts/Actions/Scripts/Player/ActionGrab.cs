using UnityEngine;

public class ActionGrab : ActionBaseInteract
{
    public ActionGrab(float _interactionDistance)
    {
        interactionDistance = _interactionDistance;
    }

    public override void Do(Model m)
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
            GameObject flingItemModel = (item.item is FlingableItem) ? model.nearbyObject.gameObject : null;
            model.inv.AddItem(item.item, flingItemModel);
            model.nearbyObject = null;
            model.animator.SetTrigger("pickUp");
        }
    }
}
