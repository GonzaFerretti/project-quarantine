using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firecracker : MonoBehaviour
{
    public float turnOffDelay;
    public float staggerDuration;

    void Start()
    {
        StartCoroutine(TurnOff());
    }

    public IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(turnOffDelay);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider c)
    {
        if(c.GetComponent<ModelPatrol>())
        {
            c.GetComponent<ModelPatrol>().Stagger(staggerDuration);
        }
    }
}
