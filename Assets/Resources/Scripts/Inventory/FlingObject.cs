using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingObject : Model, IMakeNoise
{
    public float noiseValue;
    public ActionWrapper[] collisionAction;
    public Rigidbody rb;
    Collider col;
    public TentativeFlingObjectFeedback tentativeFeedback;
    TentativeFlingObjectFeedback _myTentativeFeedback;
    MeshFilter _myMesh;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void SetAttributes(Mesh mesh)
    {
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject)
        {
            for (int i = 0; i < collisionAction.Length; i++)
            {
                if (collisionAction[i].action == null) collisionAction[i].SetAction();
                collisionAction[i].action.Do(this);
            }

            if (!_myTentativeFeedback) _myTentativeFeedback = Instantiate(tentativeFeedback);
            else _myTentativeFeedback.gameObject.SetActive(true);
            _myTentativeFeedback.transform.position = transform.position;
            _myTentativeFeedback.StartCoroutine(_myTentativeFeedback.TurnOff());
            gameObject.SetActive(false);
        }
    }

    public float GetNoiseValue()
    {
        return noiseValue;
    }
}
