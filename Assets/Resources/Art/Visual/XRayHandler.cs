using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayHandler : MonoBehaviour
{
    public Shader shader;
    public Color color;

    public float fresnelBias = 0;
    public float fresnelScale = 1;
    public float fresnelPower = 5;

    ModelEnemy[] _enemyList;
    Camera _myCamera;

    private void Start()
    {
        _enemyList = new ModelEnemy[FindObjectsOfType<ModelEnemy>().Length];
        _enemyList = FindObjectsOfType<ModelEnemy>();
        _myCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Shader.SetGlobalColor("_XRayColor", color);
        Shader.SetGlobalFloat("_FresnelBias", fresnelBias);
        Shader.SetGlobalFloat("_FresnelScale", fresnelScale);
        Shader.SetGlobalFloat("_FresnelPower", fresnelPower);
        for (int i = 0; i < _enemyList.Length; i++)
        {
            Vector3 screenPoint = _myCamera.WorldToViewportPoint(_enemyList[i].transform.position);
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                RaycastHit ray;
                if (Physics.Raycast(transform.position, (_enemyList[i].transform.position - transform.position).normalized, out ray, Vector3.Distance(transform.position,_enemyList[i].transform.position)))
                {
                    if (ray.collider.gameObject != _enemyList[i].gameObject)
                        _enemyList[i].GetComponentInChildren<SkinnedMeshRenderer>().material.shader = shader;
                }
            }
        }
    }
}