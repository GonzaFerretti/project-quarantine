using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PatrolAutoNode : MonoBehaviour
{
    public GameObject nodePrefab;
    public ModelPatrol model;
#if UNITY_EDITOR
    private void Start()
    {
        model = GetComponent<ModelPatrol>();
    }
    public void Update()
    {
        enabled = !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode;
        if (!model.node)
        {
            GameObject newNode = Instantiate(nodePrefab, transform.parent);
            newNode.transform.position = model.transform.position + nodePrefab.GetComponent<Renderer>().bounds.extents.y * Vector3.up;
            model.node = newNode.GetComponent<PatrolNode>();
            newNode.name = "AutoPatrolNode" +" (" + model.name + ")";
        }
        else
        {
            model.node.transform.position = model.transform.position + nodePrefab.GetComponent<Renderer>().bounds.extents.y * Vector3.up;
        }
    }
#endif
}
