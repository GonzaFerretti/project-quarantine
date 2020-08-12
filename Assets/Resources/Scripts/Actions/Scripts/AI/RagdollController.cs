using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rbs;
    Collider[] cols;
    Animator anim;
    public bool testKnock;
    public bool testWake;
    public Vector3 pushForce;
    public Rigidbody head;
    public ModelPatrol mPatrol;
    public Transform dragPivot;

    public void Init(ModelPatrol m)
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        cols = GetComponentsInChildren<Collider>().Union(GetComponentsInParent<Collider>()).ToArray();
        anim = GetComponent<Animator>();
        foreach (Rigidbody rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }/*
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }*/
        mPatrol = m;
    }

    public void Update()
    {
        if (testKnock)
        {
            KnockOut(pushForce);
            testKnock = false;
        }
        if (testWake)
        {
            WakeUp();
            testWake = false;
        }
    }

    public void WakeUp()
    {
        mPatrol.GetComponent<NavMeshAgent>().enabled = true;
        mPatrol.SetFOVcones(true);
        foreach (Rigidbody rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }/*
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }*/
        anim.enabled = true;
        anim.SetTrigger("awake");
        mPatrol.EnableEvents();
        //tentative
        mPatrol.controller = mPatrol.standardController;
        mPatrol.KOManager.RemovePatrol(mPatrol);
        mPatrol.GetComponent<InteractableObject>().requiredAction = null;
        ChangeLayer(mPatrol.gameObject, LayerMask.NameToLayer("Enemy"));
    }

    public void PickUp()
    {
        anim.enabled = true;
        foreach (Rigidbody rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void KnockOut(Vector3 force)
    {
        mPatrol.SetFOVcones(false);
        foreach (Rigidbody rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }
        head.AddForce(force, ForceMode.Impulse);
        anim.SetTrigger("sleep");
        anim.enabled = false;
        mPatrol.DisableEvents();
        mPatrol.GetComponent<NavMeshAgent>().enabled = false;
        //mPatrol.KOManager.AddPatrol(mPatrol);
        ChangeLayer(mPatrol.gameObject, LayerMask.NameToLayer("EnemyDown"));
    }

    private void ChangeLayer(GameObject root, int layer)
    {
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                if (VARIABLE.gameObject.name != "MapIcon")
                {
                    VARIABLE.gameObject.layer = layer;
                    Searcher(VARIABLE.gameObject, layer);
                }
            }
        }
    }

    private void Searcher(GameObject root, int layer)
    {
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                if (VARIABLE.gameObject.name != "MapIcon")
                {
                    VARIABLE.gameObject.layer = layer;
                    Searcher(VARIABLE.gameObject, layer);
                }
            }
        }
    }

}
