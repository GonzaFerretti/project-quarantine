using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFling : IAction
{
    public void Do(Model m)
    {
        //m.flingObject.SetAttributes();
        Vector3 dir;
        float strength;
        ModelChar mc = m as ModelChar;

        if (m is ModelPlayable)
        {
            ModelPlayable mp = (m as ModelPlayable);
            dir = new Vector3(mp.flingSpotlight.transform.forward.x, 0, mp.flingSpotlight.transform.forward.z);
            m.transform.forward = dir;
            //tentative
            mp.flingObject.transform.position = m.transform.position + dir + new Vector3(0, m.transform.localScale.y, 0);

            float dis = Vector3.Distance(mp.GetComponentInChildren<FlingSpotLight>().point, m.transform.position);

            if (dis < mp.strength)
                strength = dis;
            else strength = mp.strength;
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
        mc.flingObject.rb.AddForce(Vector3.up * mc.strength / 2, ForceMode.Impulse);
    }

}
