using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Character")]
public class CharacterAttributes : ScriptableObject
{
    public float sneakSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float strength;
    public ActionWrapper[] innateActions;
    public Mesh mesh;
    public Material[] materials;
}
