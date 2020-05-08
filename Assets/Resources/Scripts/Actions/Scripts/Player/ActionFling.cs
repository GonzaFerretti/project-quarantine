using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFling : IAction
{
    float _upwardStrength;


    public ActionFling(float upwardStrength)
    {
        _upwardStrength = upwardStrength;
    }

    public void Do(Model m)
    {
        //m.flingObject.SetAttributes();
        float strength;
        Vector3 dir;
        ModelChar mc = m as ModelChar;
        if (mc.flingObject.gameObject.activeSelf) return;

        if (m is ModelPlayable)
        {
            ModelPlayable mp = (m as ModelPlayable);
            if (mp.inv.items.Count == 0) return;

            //tentative
            List<FlingableItem> flingableItems = new List<FlingableItem>();

            for (int i = 0; i < mp.inv.items.Count; i++)
            {
                if (mp.inv.items[i] is FlingableItem)
                    flingableItems.Add(mp.inv.items[i] as FlingableItem);
            }

            if (flingableItems.Count == 0) return;

            //if (!(mp.currentlySelectedItem is FlingableItem)) return;
            dir = new Vector3(mp.flingSpotlight.transform.forward.x, 0, mp.flingSpotlight.transform.forward.z);
            m.transform.forward = dir;
            //tentative
            mp.flingObject.transform.position = m.transform.position + dir + new Vector3(0, m.transform.localScale.y, 0);

            float dis = Vector3.Distance(mp.GetComponentInChildren<FlingSpotLight>().point, m.transform.position);

            if (dis < mp.strength)
                strength = dis;
            else strength = mp.strength;

            //tentative
            mc.flingObject.SetAttributes(flingableItems[0].itemModel);
            mp.inv.RemoveItem(flingableItems[0]);
        }
        else
        {
            ModelEnemy me = (m as ModelEnemy);
            dir = (me.target.transform.position - me.transform.position).normalized;
            me.transform.forward = dir;
            //tentative
            me.flingObject.transform.position = m.transform.position + dir + new Vector3(0, m.transform.localScale.y, 0);

            float dis = Vector3.Distance(me.target.transform.position, m.transform.position);

            if (dis < me.strength)
                strength = dis;
            else strength = me.strength;
        }

        mc.flingObject.gameObject.SetActive(true);
        mc.flingObject.rb.velocity = Vector3.zero;
        mc.flingObject.rb.angularVelocity = Vector3.zero;
        mc.flingObject.rb.AddForce(dir * strength, ForceMode.Impulse);
        mc.flingObject.rb.AddTorque(mc.flingObject.transform.forward, ForceMode.Impulse);
        mc.flingObject.rb.AddForce(Vector3.up * _upwardStrength / 2, ForceMode.Impulse);        
    }
}
