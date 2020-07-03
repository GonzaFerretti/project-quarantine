using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceUI : MonoBehaviour
{
    public Resource Resource;
    public abstract void UpdateUI(int current, int required);
}
