using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class lampFlickering : MonoBehaviour
{
    Light lampLight;
    public bool willFlicker;
    public float usualIntensity;
    public float waitTime;
    public float waitTimer = 0;
    public float flickersMax;
    public float flickers;
    public float minFlickers, maxFlickers;
    public float minWait, maxWait;
    public float flickerPeriod;
    public float flickerTimer;
    private void Start()
    {
        lampLight = GetComponentInChildren<Light>();
        usualIntensity = lampLight.intensity;
        flickers = flickersMax;
    }
    private void Update()
    {
#if UNITY_EDITOR
        lampLight = GetComponentInChildren<Light>();
#endif
        if (willFlicker)
        {
            if (waitTimer < waitTime)
            {
                waitTimer += Time.deltaTime;
            }
            else
            {
                if (flickerTimer < flickerPeriod)
                {
                    flickerTimer += Time.deltaTime;
                    lampLight.intensity = 0;
                }
                else
                {
                    lampLight.intensity = usualIntensity;
                    flickerTimer = 0;
                    flickers--;
                    if (flickers <= 0)
                    {
                        flickers = Random.Range(minFlickers, maxFlickers);
                        waitTimer = 0;
                        waitTime = Random.Range(minWait, maxWait);
                    }
                }
            }
        }
    }
}
