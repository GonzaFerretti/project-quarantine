using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/IndoorAttributes")]
public class IndoorAttributes : ScriptableObject
{
    public int storyAmount;
    public float height;
    public ILayout layout;
}
