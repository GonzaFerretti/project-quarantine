using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Flingable Item")]
public class FlingableItem : Item
{
    public List<ActionWrapper> collisionActions;

    public override Item cloneItem()
    {
        FlingableItem newItem = new FlingableItem();
        newItem.displayName = displayName;
        newItem.description = description;
        newItem.allowingActions = new List<ActionWrapper>();
        newItem.allowingActions = allowingActions;
        newItem.collisionActions = new List<ActionWrapper>();
        newItem.collisionActions = collisionActions;
        return newItem;
    }
}
