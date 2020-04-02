using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceMinimapShader : MonoBehaviour
{
    public Shader minimapShader;
    private Camera minimapCam;
    void Start()
    {
        minimapCam = GetComponent<Camera>();
        minimapCam.SetReplacementShader(minimapShader,"");
    }
}
