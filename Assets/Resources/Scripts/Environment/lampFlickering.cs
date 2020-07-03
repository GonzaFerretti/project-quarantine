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
    private float waitTime;
    private float waitTimer = 0;
    private float flickersMax;
    private float flickers;
    [Header("Amount of flickers")]
    public float minFlickers;
    public float maxFlickers;
    [Header("Time inbetween multiple flickers")]
    public float minWait;
    public float maxWait;
    [Header("Off period for flickers")]
    public float flickerPeriod;
    private float flickerTimer;
    private void Start()
    {
        lampLight = GetComponentInChildren<Light>();
        flickers = Random.Range(minFlickers, maxFlickers);
        waitTime = Random.Range(minWait, maxWait);
        waitTimer = 0;
        flickers = 0;
        flickerTimer = 0;
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
