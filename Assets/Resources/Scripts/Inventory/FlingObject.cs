using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingObject : Model, IMakeNoise
{
    public float noiseValue;
    public List<ActionWrapper> collisionActions;
    public Rigidbody rb;
    public List<GameObject> objectsNotToCollideWith = new List<GameObject>();
    Collider col;
    public TentativeFlingObjectFeedback tentativeFeedback;
    TentativeFlingObjectFeedback _myTentativeFeedback;
    public float soundRegulator;
    MeshFilter _myMesh;
    public SoundClip clip;
    public TrailRenderer trailren;
    public float currentRotation;
    public Vector3 currentBaseRotation;
    public float rotationSpeed;

    public GameObject impactedObject;

    private void Awake()
    {
        _myMesh = GetComponent<MeshFilter>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        sm = FindObjectOfType<SoundManager>();
    }

    public void SetAttributes(FlingObjectInfo objectInfo)
    {
        _myMesh.mesh = objectInfo._mesh;
        GetComponent<MeshRenderer>().material = objectInfo._material;
        transform.localScale = objectInfo._originalScale;
        collisionActions = objectInfo._collisionActions;
    }

    public void Init()
    {
        currentBaseRotation = transform.eulerAngles;
        trailren.Clear();
        currentRotation = 0;
        trailren.transform.localPosition = new Vector3(0,0,_myMesh.mesh.bounds.extents.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject && !other.isTrigger)
        {
            if (!sm) sm = FindObjectOfType<SoundManager>();
            sm.Play(clip);
            impactedObject = other.gameObject;
            for (int i = 0; i < collisionActions.Count; i++)
            {
                if (collisionActions[i].action == null) collisionActions[i].SetAction();
                collisionActions[i].action.Do(this);
            }
            if (!_myTentativeFeedback) _myTentativeFeedback = Instantiate(tentativeFeedback);
            else _myTentativeFeedback.gameObject.SetActive(true);
            _myTentativeFeedback.transform.position = transform.position;
            _myTentativeFeedback.StartCoroutine(_myTentativeFeedback.TurnOff());
            trailren.Clear();
            gameObject.SetActive(false);
            foreach (GameObject go in objectsNotToCollideWith)
            {
                if (go)
                {
                    Physics.IgnoreCollision(go.GetComponent<Collider>(), col, false);
                }
            }
            objectsNotToCollideWith = new List<GameObject>();
        }
    }

    private void OnCollisionEnter(Collision c)
    {
    }

    public void Update()
    {
        if (gameObject.activeSelf)
        {
            currentRotation += rotationSpeed * Time.deltaTime;
            transform.eulerAngles = currentBaseRotation + new Vector3(currentRotation, 0, 0);
        }
    }

    public float GetNoiseValue()
    {
        return noiseValue;
    }
}
