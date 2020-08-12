using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{   
    public string displayName;
    public string description;
    public Sprite icon;
    public bool isStackable;
    public List<ActionWrapper> allowingActions = new List<ActionWrapper>();
    public Resource resource;
    public int amountPerResource;
    public Color seeThroughColor;

    public virtual Item cloneItem()
    {
        Item newItem = new Item();
        newItem.displayName = displayName;
        newItem.description = description;
        newItem.allowingActions = new List<ActionWrapper>();
        newItem.allowingActions = allowingActions;
        newItem.amountPerResource = amountPerResource;
        newItem.resource = resource;
        newItem.icon = icon;
        newItem.isStackable = isStackable;
        return newItem;
    }
}
