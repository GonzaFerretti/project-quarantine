using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(menuName = "New Character")]
public class CharacterAttributes : ScriptableObject
{
    public float sneakSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float strength;
    public ActionWrapper[] innateActions;
    public GameObject characterModel;
    public Material[] materials;
    public AnimatorController animations;
}
