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
        ModelChar mc = m as ModelChar;
        mc.animator.SetTrigger("fling");
        (m as ModelPlayable).CheckFlingObjectExistsOrCreate();
        m.StartCoroutine(waitForAnimationAndFling(mc));
    }

    IEnumerator logSpeed(FlingObject fo)
    {
        Rigidbody rb = fo.GetComponent<Rigidbody>();
        do
        {
            yield return null;
        } while (rb.velocity == Vector3.zero);
        Debug.Log(rb.velocity);
    }

    IEnumerator waitForAnimationAndFling(Model m)
    {
        ModelChar mc = m as ModelChar;
        AnimCheck check = GameObject.FindObjectOfType<AnimCheck>();
        do
        {
            yield return null;
        } while (!check.hasReachedPoint);
        //m.flingObject.SetAttributes();
        float strength;
        Vector3 dir;
        if (mc.flingObject.gameObject.activeSelf) yield break;
        check.hasReachedPoint = false;
        if (m is ModelPlayable)
        {
            ModelPlayable mp = (m as ModelPlayable);
            Vector3 handPosition = GameObject.FindGameObjectWithTag("throwingHand").transform.position;
            if (mp.inv.items.Count == 0) yield break;

            //tentative
            List<FlingableItem> flingableItems = new List<FlingableItem>();

            for (int i = 0; i < mp.inv.items.Count; i++)
            {
                if (mp.inv.items[i] is FlingableItem)
                    flingableItems.Add(mp.inv.items[i] as FlingableItem);
            }

            if (flingableItems.Count == 0) yield break;

            //if (!(mp.currentlySelectedItem is FlingableItem)) return;
            dir = new Vector3(mp.flingSpotlight.transform.position.x - handPosition.x, 0, mp.flingSpotlight.transform.position.z - handPosition.z).normalized;
            m.transform.forward = dir;
            //tentative
            mp.flingObject.transform.position = handPosition + dir + new Vector3(0, m.transform.localScale.y, 0);

            float dis = Vector3.Distance(mp.GetComponentInChildren<FlingSpotLight>().point, handPosition);

            if (dis < mp.strength)
                strength = dis;
            else strength = mp.strength;

            //tentative
            mc.flingObject.SetAttributes(flingableItems[0].flingItemRuntimeInfo);
            mc.flingObject.Init();
            mp.inv.RemoveItem(flingableItems[0]);
            (mp.controller as FlingController).DisableFling();
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
        FlingObject flingObject = mc.flingObject;
        m.StartCoroutine(logSpeed(flingObject));
        flingObject.transform.forward = dir.normalized;
        flingObject.gameObject.SetActive(true);
        flingObject.rb.velocity = Vector3.zero;
        flingObject.rb.angularVelocity = Vector3.zero;
        Vector3 forceToApply = dir * strength + Vector3.up * _upwardStrength / 2;
        flingObject.rb.AddForce(forceToApply, ForceMode.VelocityChange);
        /*flingObject.rb.AddForce(dir * strength, ForceMode.Impulse);
        flingObject.rb.AddTorque(mc.flingObject.transform.forward, ForceMode.Impulse);
        flingObject.rb.AddForce(Vector3.up * _upwardStrength / 2, ForceMode.Impulse);*/

        Collider flingCollider = flingObject.GetComponent<Collider>();
        foreach (GameObject go in (m as ModelPlayable).GetFlingObstacleObjects())
        {
            if (go)
            {
                flingObject.objectsNotToCollideWith.Add(go);
                Physics.IgnoreCollision(go.GetComponent<Collider>(), flingCollider, true);
            }
        }
    }
}
