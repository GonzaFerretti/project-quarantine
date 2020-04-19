using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{   
    public string _displayName;
    public string _description;
    public List<ActionWrapper> allowingActions = new List<ActionWrapper>();
    public Item cloneItem()
    {
        Item newItem = new Item();
        newItem._displayName = _displayName;
        newItem._description = _description;
        newItem.allowingActions = new List<ActionWrapper>();
        newItem.allowingActions = allowingActions;
        return newItem;
    }
}
