using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSwapFlingItem : BaseAction
{
    FlingableItem bottle;
    FlingableItem rock;

    public ActionSwapFlingItem(FlingableItem _bottle, FlingableItem _rock)
    {
        bottle = _bottle;
        rock = _rock;
    }

    public override void Do(Model m)
    {
        ModelPlayable mp = (m as ModelPlayable);
        int bottleAmount = mp.inv.GetItemCount(bottle);
        int rockAmount = mp.inv.GetItemCount(rock);

        if ((bottleAmount > 0 && rockAmount == 0) || (rockAmount > 0 && bottleAmount == 0) || (rockAmount == 0 && rockAmount == bottleAmount))
        {

            if (rockAmount > 0)
            {
                mp.inv.UpdateUI(rock, false, false, (rockAmount > 0 && bottleAmount == 0), true);
                if (bottleAmount == 0)
                {
                    mp.inv.currentlySelectedItem = rock;
                    mp.inv.UpdateUI(bottle, false, false, false, true);
                }
            }
            if (bottleAmount > 0)
            {
                mp.inv.UpdateUI(bottle, false, false, (bottleAmount > 0 && rockAmount == 0), true);
                if (rockAmount == 0) {
                    mp.inv.currentlySelectedItem = bottle;
                    mp.inv.UpdateUI(rock, false, false, false, true);
                }
            }
        }
        else if (bottleAmount > 0 && rockAmount > 0)
        {
            if (mp.inv.currentlySelectedItem == bottle)
            {
                mp.inv.currentlySelectedItem = rock;
                mp.inv.UpdateUI(rock, false, false, true, true);
                mp.inv.UpdateUI(bottle, false, false, false, true);
            }
            else
            {
                mp.inv.currentlySelectedItem = bottle;
                mp.inv.UpdateUI(rock, false, false, false, true);
                mp.inv.UpdateUI(bottle, false, false, true, true);
            }
        }
    }
}
