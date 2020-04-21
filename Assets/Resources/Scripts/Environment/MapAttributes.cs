using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/IndoorAttributes")]
public class MapAttributes : ScriptableObject
{
    public string mapName;
    public int storyAmount;
    public int doorAmount;
    public float height;
    public ILayout layout;
}
