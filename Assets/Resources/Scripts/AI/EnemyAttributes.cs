using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Enemy")]
public class EnemyAttributes : ScriptableObject
{
    public float alertRange;
    public float suspectRange;
    public float angle;
    public float alertDistance;
    public float hearingDistance;
    public Mesh mesh;
}
