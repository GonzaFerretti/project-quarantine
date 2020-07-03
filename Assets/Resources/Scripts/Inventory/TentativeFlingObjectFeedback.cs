using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentativeFlingObjectFeedback : MonoBehaviour
{
    public float timer;
    float _ogSpeed;
    float _ogRadius;

    public float speed;
    public float speedUnit;
    public float radius;
    public float radiusUnit;
    public float maxRadius;

    ParticleSystem _myParticleSystem;    

    private void Start()
    {
        _myParticleSystem = GetComponent<ParticleSystem>();
        var main = _myParticleSystem.main.startSpeed;
        var shape = _myParticleSystem.shape;
        _ogSpeed = main.constant;
        _ogRadius = shape.radius;
    }

    private void Update()
    {
        Grow();
    }

    public void Grow()
    {
        var main = _myParticleSystem.main;
        var shape = _myParticleSystem.shape;
        speed -= speedUnit * Time.deltaTime;
        main.startSpeed = speed; 
        shape.radius += radiusUnit * Time.deltaTime;
    }

    public void SetOriginalParam()
    {
        speed = _ogSpeed;
        var shape = _myParticleSystem.shape.radius;
        shape = _ogRadius;
    }

    public IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
        SetOriginalParam();
    }
}