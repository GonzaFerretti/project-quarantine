using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLight : MonoBehaviour
{
    public float interval;
    public float timer;
    public bool isRed;
    public Material mat, red, blue;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>= interval)
        {
            ChangeColor();
            timer = 0;
        }
    }
    private void ChangeColor()
    {
        if (isRed)
        {
            GetComponent<MeshRenderer>().material = blue;
            isRed = false;
        }
        else
        {
            GetComponent<MeshRenderer>().material = red;
            isRed = true;
        }
    }
}
