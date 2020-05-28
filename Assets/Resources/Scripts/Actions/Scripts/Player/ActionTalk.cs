using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTalk : IAction
{
    public void Do(Model m)
    {
        //Stop Game Timer

        ModelPlayable mp = m as ModelPlayable;
        if (mp.nearbyObject != null && m is ModelPlayable)
        {
            ModelPlayable model = m as ModelPlayable;
            Vector3 baseDirection = (model.nearbyObject.transform.position - model.transform.position).normalized;
            Vector3 finalDirection = new Vector3(baseDirection.x, 0, baseDirection.z);
            ModelNPC _npc = model.nearbyObject.GetComponent<ModelNPC>();
            model.transform.forward = finalDirection;

            if (mp.controller == mp.usualController)
                mp.controller = mp.talkController;

            if (_npc.currentLine == _npc.maxLine)
            {
                mp.controller = mp.usualController;
            }
        }
    }
}
