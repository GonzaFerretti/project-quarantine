using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public ModelPlayable player;
    public float camDistance;

    public Vector2 smooth;

    private void LateUpdate()
    {
        if (player)
            FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 newPos;

        newPos.x = ((player.transform.position.x - camDistance - transform.position.x) / smooth.x) * Time.deltaTime;
        newPos.y = 0;
        newPos.z = ((player.transform.position.z - camDistance - transform.position.z) / smooth.y) * Time.deltaTime;

        transform.position += newPos;
    }
}
