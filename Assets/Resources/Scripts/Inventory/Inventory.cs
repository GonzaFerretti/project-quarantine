using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items;
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

    public void AddItem(Item item)
    {
        Debug.Log("itemAdded");
        items.Add(item);
        foreach (ActionWrapper action in item.allowingActions)
        {
            if (!_model.gainedActions.Contains(action))
                (_model as ModelPlayable).gainedActions.Add(action);
            if (action.actionKey && !_model.gainedActionKeyLinks.Contains(action.actionKey))
                (_model as ModelPlayable).gainedActionKeyLinks.Add(action.actionKey);
        }
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        { items.Remove(item);
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
}
