using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items;
    public FlingableItem currentlySelectedItem;
    ModelChar _model;

    public void initializeInventory(ModelChar model)
    {
        _model = model;
        foreach (Item item in items)
        {
            foreach (ActionWrapper action in item.allowingActions)
            { 
            if (!_model.gainedActions.Contains(action))
                (_model as ModelPlayable).gainedActions.Add(action);
            
            if (action.actionKey && !_model.gainedActionKeyLinks.Contains(action.actionKey))
                (_model as ModelPlayable).gainedActionKeyLinks.Add(action.actionKey);
            }
        }
    }

    public void AddItem(Item item, GameObject objectObtainedFrom = null)
    {
        if (item is FlingableItem && !currentlySelectedItem) currentlySelectedItem = item as FlingableItem;
        Item itemToAdd = item;
        if (objectObtainedFrom) (itemToAdd as FlingableItem).flingItemRuntimeInfo = new FlingObjectInfo(objectObtainedFrom);
        items.Add(itemToAdd);
        try { 
            foreach (ActionWrapper action in itemToAdd.allowingActions)
            {
                if (!_model.gainedActions.Contains(action))
                    (_model as ModelPlayable).gainedActions.Add(action);
                if (action.actionKey && !_model.gainedActionKeyLinks.Contains(action.actionKey))
                    (_model as ModelPlayable).gainedActionKeyLinks.Add(action.actionKey);
            }
            
        }
        catch {}
        if (itemToAdd.resource != null)
        {
            ResourceManager.AddToResourceDict(itemToAdd.resource.resourceName, itemToAdd.amountPerResource, ref ResourceManager.currentResources);
        }
        if (itemToAdd.icon != null)
        {
            UpdateUI(itemToAdd, true, false, itemToAdd == currentlySelectedItem, false);
        }
    }

    public void UpdateAllUI()
    {
        List<Item> itemsUnique = items.GroupBy(item => item.displayName).Select(g => g.First()).ToList();
        foreach (Item item in itemsUnique)
        {
            if (item.icon != null)
            {
                UpdateUI(item, true, true, item == currentlySelectedItem, false);
            }
        }
    }

    public void UpdateUI(Item item, bool isAdding, bool isReloading, bool isSelected, bool isSelecting)
    {
        Item _item = item;
        int count = GetItemCount(item);
        bool _isAdding = isAdding;
        bool _isReloading = isReloading;
        FindObjectOfType<UiManager>().UpdateItem(item, GetItemCount(item), isAdding, isReloading, isSelected, isSelecting);
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            UpdateUI(item, false, false, item == currentlySelectedItem, false);
        }
        foreach (ActionWrapper action in item.allowingActions)
        {
            if (_model.gainedActions.Contains(action))
                (_model as ModelPlayable).gainedActions.Remove(action);
            if (action.actionKey && _model.gainedActionKeyLinks.Contains(action.actionKey))
                (_model as ModelPlayable).gainedActionKeyLinks.Remove(action.actionKey);
        }
    }

    public List<Item> GetAllItems()
    {
        return items;
    }

    public Inventory cloneInvTemplate()
    {
        Inventory newInventory = Instantiate(this);
        return newInventory;
    }

    //tentative
    public int GetItemCount(Item item)
    {
        int count = 0;
        foreach (Item _item in items)
        {
            if (_item == item)
            {
                count++;
            }
        }
        return count;
    }
}
