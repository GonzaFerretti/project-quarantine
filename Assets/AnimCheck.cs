using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCheck : MonoBehaviour
{
    public bool hasReachedPoint = false;
    public void SetAnim()
    {
        hasReachedPoint = true;
    }

    public void ResetAnim()
    {
        hasReachedPoint = false;
    }
}
