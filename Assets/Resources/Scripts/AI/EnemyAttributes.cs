using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(menuName ="New Enemy")]
public class EnemyAttributes : ScriptableObject
{
    public float alertRange;
    public float suspectRange;
    public float angle;
    public float alertDistance;
    public GameObject characterModel;
    public AnimatorController animations;
}
