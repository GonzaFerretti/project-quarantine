using UnityEngine;

public class ActionTalk : ActionBaseInteract
{
    public ActionTalk(float _interactionDistance)
    {
        interactionDistance = _interactionDistance;
    }

    public override void Do(Model m)
    {
        //Stop Game Timer
        if (m is ModelPlayable)
        {
            RaycastHit hit = new RaycastHit();
            ModelPlayable mp = m as ModelPlayable;
            Physics.Raycast(mp.transform.position + new Vector3(0, mp.GetComponent<CapsuleCollider>().height / 2, 0), mp.transform.forward, out hit, interactionDistance);

            if (hit.collider && hit.collider.GetComponent<ModelNPC>())
            {
                if (mp.controller == mp.usualController)               
                    mp.controller = mp.talkController;
                
                ModelNPC _npc = hit.collider.GetComponent<ModelNPC>();
                Vector3 baseDirection = (_npc.transform.position - mp.transform.position).normalized;
                Vector3 finalDirection = new Vector3(baseDirection.x, 0, baseDirection.z);
                m.transform.forward = finalDirection;
                if (_npc.currentLine == _npc.maxLine)
                    mp.controller = mp.usualController;
            }
        }
    }
}
