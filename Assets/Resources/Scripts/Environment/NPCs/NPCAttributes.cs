using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/NPCs/Attributes")]
public class NPCAttributes : ScriptableObject
{
    public GameObject characterModel;
    public List<Resource> resources;
    public List<NPCDialog> dialog;
    public RuntimeAnimatorController animations;
}
