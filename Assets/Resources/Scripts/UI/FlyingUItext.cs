using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlyingUItext : MonoBehaviour
{
    bool isMoving = false;
    float modTextDistance;
    float modTextTime;
    float startTime;
    public TextMeshProUGUI textComp;
    Color startColor = Color.white;
    Color endColor = new Color(1,1,1,0);
    public void StartMoving(float distance, float time, string text)
    {
        isMoving = true;

        modTextDistance = distance;
        modTextTime = time;
        startTime = Time.time;
        textComp.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float currentTime = Time.time - startTime;
            if (currentTime < modTextTime)
            {
                //Debug.Log(currentTime);
                GetComponent<RectTransform>().anchoredPosition += Vector2.up * (modTextDistance / modTextTime) * Time.deltaTime;
                textComp.color = Color.Lerp(startColor, endColor, currentTime/modTextTime);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
