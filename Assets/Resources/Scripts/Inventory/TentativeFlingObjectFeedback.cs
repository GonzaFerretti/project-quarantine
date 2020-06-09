using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentativeFlingObjectFeedback : MonoBehaviour
{
    public float timer;
    public float growthAmount;
    float _myWidth;
    float _myHeigth;
    float _myBreadth;

    private void Update()
    {
        Grow();
    }

    public void Grow()
    {
        _myWidth = _myHeigth = _myBreadth += growthAmount * Time.deltaTime;
        transform.localScale = new Vector3(_myWidth, _myHeigth, _myBreadth);
    }

    public void SetOriginalParam()
    {
        _myWidth = _myHeigth = _myBreadth = 1;
        transform.localScale = Vector3.one;
    }

    public IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
        SetOriginalParam();
    }
}
