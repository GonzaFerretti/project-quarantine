using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamara : MonoBehaviour
{
    public float newx, max, min, timer, speed, additionalMult, stasis;

    // Start is called before the first frame update
    void Start()
    {
        newx = min ;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > stasis)
        {
            if (newx > max || newx < min)
            {
                speed = -speed;
                additionalMult = 1;
                timer = 0;
            }

            newx += Time.deltaTime * speed;
            transform.position = new Vector3(newx, transform.position.y, transform.position.z);
        }

    }
}
