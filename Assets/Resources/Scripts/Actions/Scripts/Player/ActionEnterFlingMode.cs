using System.Collections.Generic;
using UnityEngine;
public class ActionEnterFlingMode : IAction
{
    public void Do(Model m)
    {
        ModelPlayable mp = m as ModelPlayable;

        List<FlingableItem> myflingables = new List<FlingableItem>();
        for (int i = 0; i < mp.inv.items.Count; i++)
        {
            if (mp.inv.items[i] is FlingableItem && !myflingables.Contains(mp.inv.items[i] as FlingableItem))
                myflingables.Add(mp.inv.items[i] as FlingableItem);
        }

        FlingSpotLight fs = mp.flingSpotlight;

        if (mp.controller != mp.flingController)
        {
            if (myflingables.Count == 0) return;
            fs.gameObject.SetActive(true);
            fs.noiseRangeIndicator.gameObject.SetActive(true);
            fs.flingRangeIndicator.gameObject.SetActive(true);
            mp.controller = mp.flingController;
        }
        else
        {
            fs.gameObject.SetActive(false);
            fs.noiseRangeIndicator.gameObject.SetActive(false);
            fs.flingRangeIndicator.gameObject.SetActive(false);
            mp.controller = mp.usualController;
        }
    }
}
