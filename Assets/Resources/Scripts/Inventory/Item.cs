using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{   
    public string displayName;
    public string description;
    public List<ActionWrapper> allowingActions = new List<ActionWrapper>();
    public virtual Item cloneItem()
    {
        Item newItem = new Item();
        newItem.displayName = displayName;
        newItem.description = description;
        newItem.allowingActions = new List<ActionWrapper>();
        newItem.allowingActions = allowingActions;
        return newItem;
    }
}
