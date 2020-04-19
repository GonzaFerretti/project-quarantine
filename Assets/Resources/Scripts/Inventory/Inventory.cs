using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items = new List<Item>();
    ModelChar _model;
    public void initializeInventory(ModelChar model)
    {
        _model = model;
        foreach (Item item in items)
        {
            foreach (ActionWrapper action in item.allowingActions)
            { 
            if (!_model.availableActions.Contains(action))
                (_model as ModelPlayable).availableActions.Add(action);
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        foreach (ActionWrapper action in item.allowingActions)
        {
            if (!_model.availableActions.Contains(action))
                (_model as ModelPlayable).availableActions.Add(action);
        }
        
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        { items.Remove(item);
        }
        foreach (ActionWrapper action in item.allowingActions)
        {
            if (_model.availableActions.Contains(action))
                (_model as ModelPlayable).availableActions.Remove(action);
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
